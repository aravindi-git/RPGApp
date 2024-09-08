using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.Models;
using RPGApp.Utilities;
using System.Security.Claims;

namespace RPGApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly UserDetails _userDetails; 
        public CharacterController(ICharacterService characterService, UserDetails userDetails)
        {
            _characterService = characterService;
            _userDetails = userDetails;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters(_userDetails.GetUserId()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id, _userDetails.GetUserId()));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter, _userDetails.GetUserId()));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter, _userDetails.GetUserId());
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id, _userDetails.GetUserId());
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = await _characterService.AddCharacterSkill(newCharacterSkill, _userDetails.GetUserId());
            return Ok(response);
        }

    }
}
