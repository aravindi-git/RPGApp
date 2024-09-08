using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.DTOs.Weapon;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Service
{
    public class WeaponService : IWeaponService
    {
        private readonly IWeaponRepository _weaponRepository;
        public WeaponService(IWeaponRepository weaponRepository) 
        {
            _weaponRepository = weaponRepository;
        }
        public async Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon, int userId)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                response.Data = await _weaponRepository.AddWeapon(newWeapon, userId); 
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response; 
        }
    }
}
