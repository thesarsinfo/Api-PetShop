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
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        [HttpGet("{id}")]         
        public async Task<IActionResult> GetById(ulong id)
        {
            try
            {
                var client = await _clientRepository.GetByIdClientAsync(id);
                if (client == null)
                    return StatusCode(404,"O cliente não foi encontrado");
                else
                    return StatusCode(200,client);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }            
        }  
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {

                var clients = await  _clientRepository.GetAllByIdClientAsync();

                if (clients.Any() == false)
                    return StatusCode(404,"Não foi encontrado nenhum cliente");
                else
                    return StatusCode(200,clients);

            }
            catch (Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }     
        }   
        
        [HttpPost]
        public async Task<IActionResult> Post( ClientDTO clientDTO)
        {
            try
            {
                var userCargo = HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToString()
                                .Equals("roleJob",StringComparison.InvariantCultureIgnoreCase));
                if(userCargo.Value.Equals("client"))
                {
                    var cpf = ulong.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToString()
                                .Equals("id",StringComparison.InvariantCultureIgnoreCase)).Value);
                    var clientSearch = await _clientRepository.GetByIdClientAsync(cpf);
                    
                    if (clientSearch != null) 
                        return StatusCode(400,"A conta com essa id já foi cadastrada");
                    
                    var email = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type
                                .Equals("email",StringComparison.InvariantCultureIgnoreCase)).Value;

                    Client client = new();                    
                    client.CPF = cpf;
                    client.Name = clientDTO.Name;
                    client.LastName = clientDTO.LastName;
                    client.Address = clientDTO.Address;
                    client.Email = email;
                    client.Status = true;

                    _clientRepository.Add(client);
                    return await _clientRepository.SaveChangesAsync()
                            ? StatusCode(201,"O cliente foi cadastrado com sucesso")
                            : StatusCode(400,"Erro ao criar o cliente");
                }
                else
                {
                    return StatusCode(403, "Somente Usuarios com cargo client podem criar conta");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro de processamento interno");
            }

        }              

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(ulong id, ClientUpdateDTO clientDTO)
        {
            try
            {
                var client = await _clientRepository.GetByIdClientAsync(id);
                if (client != null)
                {

                    client.Name = clientDTO.Name ?? client.Name;
                    client.LastName = clientDTO.LastName ?? client.LastName;
                    client.Address = clientDTO.Address ?? client.Address;
                    client.Email = clientDTO.Email ?? client.Email;
                    _clientRepository.Update(client);
                    await _clientRepository.SaveChangesAsync();
                    return StatusCode(200,"O registro do cliente foi atualizado");
                }
                else
                {                    
                    return StatusCode(404,"O cliente não foi encontrado");
                }
            }
            catch
            {
                return StatusCode(500, "Error de processamento");
            }            
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(ulong id)
        {
            try
            {
                var client = await _clientRepository.DeleteClientByIdAsync(id);
                if(client == null)
                {
                    return StatusCode(404,"O cliente não foi encontrado");
                }
                client.Status = false;
                _clientRepository.Update(client);
                await _clientRepository.SaveChangesAsync();
                return StatusCode(200,"O cliente foi removido com sucesso");
            }
            catch (Exception)
            {
                
                return StatusCode(500,"Erro de processamento");
            }
        }
    }
}