using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository; 

        public AuthService(IAuthRepository authRepository) { 
            _authRepository = authRepository;
        }    
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            try
            {
                response.Data = await _authRepository.Login(username, password); 
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message; 
            }
            return response; 
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            try
            {
                response.Data = await _authRepository.Register(user, password);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _authRepository.UserExists(username);
        }
    }
}
