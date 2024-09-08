using Microsoft.Extensions.DependencyInjection;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.Abstractions.Services;
using RPGApp.Domain.DTOs.Fight;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Service
{
    public class FightService : IFightService
    {
        private readonly IFightRepository _fightRepository; 
        public FightService(IFightRepository fightRepository) 
        { 
            _fightRepository = fightRepository; 
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                response.Data = await _fightRepository.Fight(request); 
            }
            catch(Exception ex) 
            { 
                response.Success = false;
                response.Message = ex.Message;
            }

            return response; 
        }

        public async Task<ServiceResponse<List<HigherScoreDto>>> GetHighScore()
        {
            var response = new ServiceResponse<List<HigherScoreDto>>();

            try
            {
                response.Data = await _fightRepository.GetHighScore();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                response.Data = await _fightRepository.SkillAttack(request);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                response.Data = await _fightRepository.WeaponAttack(request);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
