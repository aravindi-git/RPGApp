using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGApp.DAL.Data;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.DAL
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CharacterRepository(DataContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }
        public async Task<List<GetCharacterResponseDto>> AddCharacter(AddCharacterRequestDto newCharacter, int userId)
        {
            var characters = new List<GetCharacterResponseDto> ();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            characters = _context.Characters
                        .Where(c => c.User!.Id == userId)
                        .Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();

            return characters;
        }

        public async Task<GetCharacterResponseDto> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill, int userId)
        {
            var characterDto = new GetCharacterResponseDto();
            var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User!.Id == userId) ?? throw new Exception("Character not found.");

            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId) ?? throw new Exception("Skill not found");

            if (character != null && skill != null)
            {
                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();

                characterDto = _mapper.Map<GetCharacterResponseDto>(character);
            }
            return characterDto;
        }

        public async Task<List<GetCharacterResponseDto>> DeleteCharacter(int id, int userId)
        {
            var characterDtos = new List<GetCharacterResponseDto>();

            var character = _context.Characters.FirstOrDefault(c => c.Id == id && c.User!.Id == userId) ??
                    throw new Exception("Character not found.");

            if(character != null )
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                characterDtos = _context.Characters
                       .Where(c => c.User!.Id == userId)
                       .Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            }

            return characterDtos;
        }

        public async Task<List<GetCharacterResponseDto>> GetAllCharacters(int userId)
        {
            var characterDtos = new List<GetCharacterResponseDto>();

            var dbCharacters = await _context.Characters
                              .Include(c => c.Weapon)
                              .Include(c => c.Skills)
                              .Where(c => c.User!.Id == userId).ToListAsync();

            characterDtos = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return characterDtos;
        }

        public async Task<GetCharacterResponseDto> GetCharacterById(int id, int userId)
        {
            var character = new GetCharacterResponseDto();
            var dbCharacter = await _context.Characters
                              .Include(c => c.Weapon)
                              .Include(c => c.Skills)
                              .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == userId);

            character = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
            return character;
        }

        public async Task<GetCharacterResponseDto> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter, int userId)
        {
            var characterDto = new GetCharacterResponseDto();
            var character = _context.Characters
                            .Include(c => c.User)
                            .FirstOrDefault(c => c.Id == updatedCharacter.Id && c.User!.Id == userId);

            if (character == null || character.User!.Id != userId)
            {
                throw new Exception("Character not found.");
            }

            _mapper.Map(updatedCharacter, character);

            await _context.SaveChangesAsync();

            characterDto = _mapper.Map<GetCharacterResponseDto>(character);

            return characterDto;
        }

    }
}
