using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.DTO;
using Clinic_Veterinaty_API.Models;
using Clinic_Veterinaty_API.Repository.Interfaces;
using Clinic_Veterinaty_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Clinic_Veterinaty_API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class UserController : ControllerBase
    {
        
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register( UserDTO userDTO)
        {
            try
            {                
                var user = await _userRepository.GetEmailUser(userDTO.Email.ToUpper());
                if (user != null) return StatusCode(200,"Email do usuário ja existe, tente outro email");
                
                if (userDTO.JobRole.ToUpper() == "CLIENT" || userDTO.JobRole.ToUpper() == "VET" )
                {
                    User userSet = new();
                    userSet.Identification = userDTO.Identification;
                    userSet.Email = userDTO.Email.ToUpper();
                    EncryptPassword encrypt = new();                   
                    userSet.Password = encrypt.EncryptPasswordMethod(userDTO.Password);
                    userSet.JobRole = userDTO.JobRole;
                    //user claims business

                    _userRepository.Add(userSet);
                    await _userRepository.SaveChangesAsync();
                    
                    return StatusCode(201,"User Create with successfully");
                }                
                return StatusCode(400,"Job Role error, try to register with CLIENT or VET");                
            }
            catch (Exception)
            {                
                return StatusCode(500,"Erro no servidor");
            }            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login( UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userRepository.GetEmailUser(userLoginDTO.Email.ToUpper());
                if (user != null)
                {
                    EncryptPassword encrypt = new();
                    var password = encrypt.EncryptPasswordMethod(userLoginDTO.Password);
                    if(user.Password.Equals(password))
                    {
                        string keySecurity = "Api_GFT_Desafio_Treino";
                        var simmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecurity));
                        var credential = new SigningCredentials(simmetricKey,SecurityAlgorithms.HmacSha256Signature);
                        var claims = new List<Claim>();
                        if (user.JobRole == "client")
                        {
                            claims.Add(new Claim("roleJob",user.JobRole));
                            claims.Add(new Claim("id",user.Identification.ToString()));
                            claims.Add(new Claim("emailuser", user.Email));
                        }
                        else
                        {
                            claims.Add(new Claim("roleJob",user.JobRole)) ; 
                            claims.Add(new Claim("id",user.Identification.ToString()));
                            claims.Add(new Claim("emailuser", user.Email));
                        }                                
                       
                       
                        var jwt = new JwtSecurityToken(
                            issuer: "vetgftbrasil",                            
                            expires: DateTime.Now.AddHours(1),
                            audience: "veterinary_clinics",
                            signingCredentials: credential,
                            claims: claims
                        );
                        return StatusCode(200,new JwtSecurityTokenHandler().WriteToken(jwt));
            
                    }
                    else
                    {
                        return StatusCode(403);
                    }                    
                }
                else
                {
                    return StatusCode(403);
                }
            }
            catch (Exception)
            {                
                return StatusCode(500);
            }           

        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Login(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var identification = ulong.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToString()
                                .Equals("id",StringComparison.InvariantCultureIgnoreCase)).Value);
                var user = await _userRepository.GetUserId(identification);
                var userEmail = await _userRepository.GetEmailUser(userUpdateDTO.Email.ToUpper());
                if(user == null) return StatusCode(404,"Usuário não encontrado");
                if(userEmail != null) return StatusCode(400,"Esse email informado já existe");

                user.Email = userUpdateDTO.Email;
                EncryptPassword encrypt = new();
                var password = encrypt.EncryptPasswordMethod(userUpdateDTO.Password);
                user.Password = password;
                user.JobRole = userUpdateDTO.JobRole;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return StatusCode (200,"Usuário atualizado com sucesso");
            }
            catch
            {
                return StatusCode(500,"Erro de processamento interno");
            }
        }  
    }
}