using Microsoft.AspNetCore.Http;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Service
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter, int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            try
            {
                serviceResponse.Data = await _characterRepository.AddCharacter(newCharacter, userId);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse; 
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill, int userId)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                serviceResponse.Data = await _characterRepository.AddCharacterSkill(newCharacterSkill, userId); 
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id, int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            try
            {
                serviceResponse.Data = await _characterRepository.DeleteCharacter(id, userId);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            try
            {
                serviceResponse.Data = await _characterRepository.GetAllCharacters(userId);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id, int userId)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                serviceResponse.Data = await _characterRepository.GetCharacterById(id, userId);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter, int userId)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                serviceResponse.Data = await _characterRepository.UpdateCharacter(updatedCharacter, userId);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
