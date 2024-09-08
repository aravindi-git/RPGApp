using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.Abstractions.Repositories
{
    public interface ICharacterRepository
    {
        Task<List<GetCharacterResponseDto>> AddCharacter(AddCharacterRequestDto newCharacter, int userId);

        Task<GetCharacterResponseDto> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill, int userId);

        Task<List<GetCharacterResponseDto>> DeleteCharacter(int id, int userId);

        Task<List<GetCharacterResponseDto>> GetAllCharacters(int userId);

        Task<GetCharacterResponseDto> GetCharacterById(int id, int userId);

        Task<GetCharacterResponseDto> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter, int userId);

        

       
    }
}
