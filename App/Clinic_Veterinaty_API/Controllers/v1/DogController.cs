using System;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.DTO;
using Clinic_Veterinaty_API.Models;
using Clinic_Veterinaty_API.Repository.Interfaces;
using Clinic_Veterinaty_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Veterinaty_API.Controllers.v1
{
    [ApiController]    
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DogController : ControllerBase
    {
       
        private readonly IDogRepository _dogRepository;

        public DogController(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDogsByClientId(ulong clientId)
        {
            try
            {
                var dogs = await _dogRepository.GetAllDogsByClientId(clientId);
                if (dogs.Any() == false)
                    return StatusCode(404,"Não existem cachorros cadastrado");
                else
                    return StatusCode(200,dogs);
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }            
        }
        [HttpGet("{idDog}")]
        public async Task<IActionResult> GetDogByClientIdDogId(int idDog, ulong clientId)
        {
            try
            {
                var dog = await _dogRepository.GetDogByClientIdDogId(clientId, idDog);
                if (dog == null)
                    return StatusCode(404,"O cachorro procurado não foi encontrado");
                else
                    return StatusCode(200,dog);
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }            
        }
        [HttpGet("getdogid/{id}")]
        public async Task<IActionResult> GetDogById(int id)
        {
            try
            {
                var dog = await _dogRepository.GetDogById(id);
                if (dog == null)
                    return StatusCode(404,"O cachorro procurado pelo id não foi encontrado");
                else
                    return StatusCode(200,dog);
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }            
        }
        [HttpGet("getalldogs")]
       // [Produces("application/json")]
        public  IActionResult GetAllDogs(int page)
        {
            try
            {
                DogApiConnector dogApiConnector = new();
                var response =  dogApiConnector.ConnectToAPI("breeds?page=",page.ToString()+"&limit=10");                    
                return StatusCode(200,response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }
        }
        [HttpGet("SearchDogBreedByName")]

        public  IActionResult GetDogByName(string name)
        {
            try
            {
                DogApiConnector dogApiConnector = new();
                var response =  dogApiConnector.ConnectToAPI("breeds/search?q=",name);                    
                return StatusCode(200,response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }
        }
        [HttpGet("GetRandomImageDogByAPI")]
        public  IActionResult GetRandomImageDogByAPI()
        {  
            try 
            {                     
                DogApiConnector dogApiConnector = new();
                var response =  dogApiConnector.ConnectToAPI("images/search","");                    
                return StatusCode(200,response);
            }
            catch
            {
                return StatusCode(500, "Erro de processamento interno");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(DogDTO dogDTO)
        {
            try 
            {
                var client  = await _dogRepository.GetByIdClientAsync(dogDTO.ClientId);
                if(client == null) return NotFound("Client not found");

                Dog dog = new Dog();
                dog.Name = dogDTO.Name;
                dog.DogWeight = dogDTO.DogWeight;
                dog.DogHeight = dogDTO.DogHeight;
                dog.DogBreed = dogDTO.DogBreed;
                dog.BirthDate = dogDTO.BirthDate;
                dog.Clients = client;
                dog.Status = true;

                _dogRepository.Add(dog);
                await _dogRepository.SaveChangesAsync();                
                return StatusCode(201,"O Cachorro foi registrado ao cliente com sucesso");
            }
            catch
            {
                return StatusCode(500, "Erro de processamento interno");
            }
        }    
        [HttpPatch("{idDog}")]
         public async Task<IActionResult> Patch(int idDog, DogUpdateDTO dogUpdateDTO)
         {
             try
            {                
                var dog = await _dogRepository.GetDogById(idDog);
                if (dog != null)
                {
                    var client  = await _dogRepository.GetByIdClientAsync(dogUpdateDTO.ClientId);

                    dog.Name = dogUpdateDTO.Name ?? dog.Name;
                    dog.DogBreed = dogUpdateDTO.DogBreed ?? dog.DogBreed;
                    dog.DogWeight = dogUpdateDTO.DogWeight != 0 ? dogUpdateDTO.DogWeight: dog.DogWeight;
                    dog.DogHeight = dogUpdateDTO.DogHeight != 0 ? dogUpdateDTO.DogHeight: dog.DogHeight;
                    
                    dog.BirthDate = dogUpdateDTO.BirthDate.Equals(DateTime.Parse("01/01/0001 00:00:00")) 
                                        ?  dog.BirthDate :  dogUpdateDTO.BirthDate;
                    
                    dog.Clients = client ?? dog.Clients;

                     _dogRepository.Update(dog);
                    await _dogRepository.SaveChangesAsync();
                    //this business rule is inform the user the data of dog is modified
                    if (client == null)
                    {
                        return StatusCode(200,"Somente o cachorro foi atualizado");
                    }
                    return StatusCode(200,"O cliente ou cachorro foi atualizado");
                }
                else
                {                    
                    return StatusCode(404, "The client dog wasn't found");
                }
            }
            catch
            {
                return StatusCode(500, "Erro interno");
            }            
         }

        [HttpDelete ("{idDog}")]
        public async Task<IActionResult> DeleteDog(int idDog, ulong clientId)        
        {
            try
            {
                var dog = await _dogRepository.GetDogByClientIdDogId(clientId, idDog);
                if (dog == null)
                    return StatusCode(404,"O cachorro do cliente não foi encontrado");
                else
                {
                    dog.Status = false;
                    _dogRepository.Update(dog);
                    await _dogRepository.SaveChangesAsync();
                    return StatusCode(200,"O cachorro do cliente foi removido com sucesso");
                }
            }
            catch(Exception)
            {
                return StatusCode(500, "Erro interno");
            }  
        }
    }
}