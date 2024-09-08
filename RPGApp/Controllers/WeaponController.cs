using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.DTOs.Weapon;
using RPGApp.Domain.Models;
using RPGApp.Utilities;

namespace RPGApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        private readonly UserDetails _userDetails;

        public WeaponController(IWeaponService weaponService, UserDetails userDetails)
        {
            _weaponService = weaponService;
            _userDetails = userDetails;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> AddWeapon(AddWeaponRequestDto newWeapon)
        {
            var response = await _weaponService.AddWeapon(newWeapon, _userDetails.GetUserId());
            return Ok(response);
        }
    }
}
