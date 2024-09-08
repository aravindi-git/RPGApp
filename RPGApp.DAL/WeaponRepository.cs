using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using RPGApp.DAL.Data;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.DTOs.Weapon;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.DAL
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WeaponRepository(DataContext context,  IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharacterResponseDto> AddWeapon(AddWeaponRequestDto newWeapon, int userId)
        {
            var characterDto = new GetCharacterResponseDto(); 

            var character = await _context.Characters
               .Include(c => c.User)
               .Include(c => c.Weapon)
               .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User!.Id == userId) ?? throw new Exception ("Character not found");

            if(character.Weapon != null )
            {
                throw new Exception("Character already has a weapon."); 
            }

            Weapon weapon = _mapper.Map<Weapon>(newWeapon);
            weapon.Character = character;

            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            characterDto = _mapper.Map<GetCharacterResponseDto>(character);

            return characterDto;
        }
    }
}
