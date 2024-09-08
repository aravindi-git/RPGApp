using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.DTOs.Weapon;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.Abstractions.Repositories
{
    public interface IWeaponRepository
    {
        Task<GetCharacterResponseDto> AddWeapon(AddWeaponRequestDto newWeapon, int userId);
    }
}
