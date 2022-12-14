using System;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.DTO;
using Clinic_Veterinaty_API.Models;
using Clinic_Veterinaty_API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Veterinaty_API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class VetCareController : ControllerBase
    {
       
        private readonly IVetCareRepository _vetCareRepository;

        public VetCareController(IVetCareRepository vetCareRepository)
        {
            _vetCareRepository = vetCareRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVetCalls()
        {
            try {
                var userCargo =  HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToString()
                                    .Equals("roleJob",StringComparison.InvariantCultureIgnoreCase));
                if(userCargo.Value.Equals("client"))
                {
                    var id = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToString()
                                .Equals("id",StringComparison.InvariantCultureIgnoreCase)).Value;
                    var client = await _vetCareRepository.GetAllVetCallsByClient(ulong.Parse(id));
                    return client.Any()
                        ? StatusCode(200, client)
                        : StatusCode(404,"Nenhum Atendimento para esse cliente foi encontrado");
                }
                else if (userCargo.Value.Equals("vet"))
                {
                    var id = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToString()
                                .Equals("id",StringComparison.InvariantCultureIgnoreCase)).Value;
                    var vet = await _vetCareRepository.GetAllVetCallsByVet(ulong.Parse(id));
                    return vet.Any()
                        ? StatusCode(200, vet)
                        : StatusCode(404,"Nenhum atendimento desse veterin??rio foi encontrado");
                }
                else
                {
                    return StatusCode(404,"Nenhum atendimento foi encontrado");
                }
            }
            catch(Exception)
            {
                return StatusCode(500,"Error de processamento interno");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(VetCareDTO vetCareDTO)
        {
            var clientDog  = await _vetCareRepository.GetDogByClientIdDogId(vetCareDTO.ClientId,vetCareDTO.DogId);
            if (clientDog == null)
            {
                return StatusCode(404,"O cliente ou cachorro n??o foram encontrado");
            }
            var vet = await _vetCareRepository.GetByIdVetAsync(vetCareDTO.VetId);
            if (vet == null)
            {
                return StatusCode(404,"O veterin??rio n??o foi encontrado");
            }
            VetCare vetCare = new();
            //business rule to Age property
            var age = DateTime.Now.Subtract(clientDog.BirthDate).TotalDays / 365.0;
            if (age < 0) return StatusCode(400,"Por favor atualize a idade de nascimento do cachorro");
           
            vetCare.Age = age;   
            vetCare.Clients = clientDog.Clients;
            vetCare.Weight = vetCareDTO.Weight;
            vetCare.Vets = vet;
            vetCare.Dogs = clientDog;
            vetCare.Hour = DateTime.Now;
            vetCare.LastDiagnosis = vetCareDTO.LastDiagnosis;
            vetCare.Coments = vetCareDTO.Coments;


            _vetCareRepository.Add(vetCare);
            await _vetCareRepository.SaveChangesAsync();
            //business rule to update DogWeight   
            
            clientDog.DogWeight = vetCareDTO.Weight;
            _vetCareRepository.Update(clientDog);
            await _vetCareRepository.SaveChangesAsync();

            return StatusCode(201,"O atendimento foi criado com sucesso");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, VetCareUpdateDTO vetCareUpdateDTO)
        {
            try
            {
                double age = 0;
                var vetCall = await _vetCareRepository.GetVetCallById(id);
                if (vetCall == null) return StatusCode(404, "Consulta n??o encontrada");
                
                var dog = await _vetCareRepository.GetDogByClientIdDogId(vetCareUpdateDTO.ClientId,vetCareUpdateDTO.DogId);
                
                if (dog != null)
                {
                    age = DateTime.Now.Subtract(dog.BirthDate).TotalDays / 365.0;
                    if (age < 0) return StatusCode(400,"Por favor atualize a idade de nascimento do cachorro");
                }
                var client = await _vetCareRepository.GetClientById(vetCareUpdateDTO.ClientId);
                var vet = await _vetCareRepository.GetByIdVetAsync(vetCareUpdateDTO.VetId);
                

                vetCall.Clients = client ?? vetCall.Clients;
                vetCall.Vets = vet ?? vetCall.Vets;
                vetCall.Dogs = dog ?? vetCall.Dogs;
                vetCall.Age = age != 0 ? age : vetCall.Age;
                vetCall.Weight = vetCareUpdateDTO.Weight != 0 ? vetCareUpdateDTO.Weight : vetCall.Weight;
                vetCall.Hour = vetCareUpdateDTO.Hour.Equals(DateTime.Parse("01/01/0001 00:00:00")) 
                                        ?  vetCall.Hour :  vetCareUpdateDTO.Hour;
                vetCall.LastDiagnosis = vetCareUpdateDTO.LastDiagnosis ?? vetCall.LastDiagnosis;
                vetCall.Coments = vetCareUpdateDTO.Coments ?? vetCall.Coments;

                if(vetCareUpdateDTO.Weight != 0 )
                {
                   var dogUpdate = await _vetCareRepository.GetDogByClientIdDogId(vetCall.Clients.CPF,vetCall.Dogs.Id);
                    _vetCareRepository.Update(dogUpdate);
                    await _vetCareRepository.SaveChangesAsync();
                }
                _vetCareRepository.Update(vetCall);
                await _vetCareRepository.SaveChangesAsync();                
                return StatusCode(200,"Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Erro de processamento interno" + ex.Message);
            }
        }

    }
}