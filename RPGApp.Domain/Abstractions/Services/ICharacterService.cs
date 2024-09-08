using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.Abstractions.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters(int userId);

        Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id, int userId);

        Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter, int userId);

        Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter, int userId);

        Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id, int userId);

        Task<ServiceResponse<GetCharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill, int userId);
    }
}
