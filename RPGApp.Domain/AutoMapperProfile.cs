using AutoMapper;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Domain.DTOs.Fight;
using RPGApp.Domain.DTOs.Skill;
using RPGApp.Domain.DTOs.Weapon;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDto>();
            CreateMap<AddCharacterRequestDto, Character>();
            CreateMap<UpdateCharacterRequestDto, Character>();
            CreateMap<AddWeaponRequestDto, Weapon>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HigherScoreDto>();
        } 
    }
}
