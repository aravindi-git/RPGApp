using RPGApp.Domain.DTOs.Fight;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.Abstractions.Repositories
{
    public interface IFightRepository
    {
        Task<AttackResultDto> WeaponAttack(WeaponAttackDto request);
        Task<AttackResultDto> SkillAttack(SkillAttackDto request);
        Task<FightResultDto> Fight(FightRequestDto request);
        Task<List<HigherScoreDto>> GetHighScore();
    }
}
