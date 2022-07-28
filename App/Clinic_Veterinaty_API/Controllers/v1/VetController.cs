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
    [Authorize]
    public class VetController : ControllerBase
    {
       private readonly IVetRepository _vetRepository;

        public VetController(IVetRepository vetRepository)
        {
            _vetRepository = vetRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vet = await _vetRepository.GetByIdVetAsync(id);
                if (vet == null)
                    return StatusCode(404,"O veterinário não encontrado");
                else
                    return StatusCode(200,vet);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }            
        }  
        [HttpGet]
        public async Task<IActionResult> GetAllVets()
        {
            try
            {

                var vets = await  _vetRepository.GetAllVetsAsync();

                if (vets.Any())
                     return StatusCode(200,vets);
                else
                    return StatusCode(404,"A lista de veterinário não foi encontrado");                   

            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }     
        }   
        
        [HttpPost]
        public async Task<IActionResult> Post(VetDTO vetDTO)
        {
            try
            {
                var userCargo = HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToString()
                                .Equals("roleJob",StringComparison.InvariantCultureIgnoreCase));
                if(userCargo.Value.Equals("vet"))
                {
                    var CRMV = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToString()
                                    .Equals("id",StringComparison.InvariantCultureIgnoreCase)).Value);
                    var searchVet = await _vetRepository.GetByIdVetAsync(CRMV);
                    if(searchVet != null)                         
                        return StatusCode(400,"O id do veterinário já existe");

                    var email = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type
                                .Equals("email",StringComparison.InvariantCultureIgnoreCase)).Value;

            
                    Vet vet = new();            
                    vet.CRMV = CRMV;
                    vet.Name = vetDTO.Name;
                    vet.LastName = vetDTO.LastName;
                    vet.Address = vetDTO.Address;
                    vet.Email = email;
                    vet.Status = true;

                    _vetRepository.Add(vet);
                    return await _vetRepository.SaveChangesAsync()
                            ? StatusCode(201,"O veterinário foi criado com sucesso")
                            : StatusCode(400,"Houve um erro ao criar o veterinário");
                }
                else
                {
                    return StatusCode(403, "Somente o usuário com cargo vet podem cadastrar os veterinarios");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }
        }              

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, VetUpdateDTO vetDTO)
        {
            try
            {
                var vet = await _vetRepository.GetByIdVetAsync(id);
                if (vet != null)
                {

                    vet.Name = vetDTO.Name ?? vet.Name;
                    vet.LastName = vetDTO.LastName ?? vet.LastName;
                    vet.Address = vetDTO.Address ?? vet.Address;
                    vet.Email = vetDTO.Email ?? vet.Email;
                    _vetRepository.Update(vet);
                    await _vetRepository.SaveChangesAsync();
                    return StatusCode(200,"O veterinário foi atualizado com sucesso");
                }
                else
                {                    
                    return StatusCode(404,"Veterinário não encontrado");
                }
            }
            catch
            {
                return StatusCode(500, "Erro interno");
            }            
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var vet = await _vetRepository.DeleteVetByIdAsync(id);
                if(vet == null)
                {
                    return StatusCode(404,"Veterinário não encontrado");
                }
                vet.Status = false;
                _vetRepository.Update(vet);
                await _vetRepository.SaveChangesAsync();
                return StatusCode(200,"Veterinário removido com sucesso");
            }
            catch (Exception)
            {
                
                return StatusCode(500, "Erro interno");

            }
        }
    }
}