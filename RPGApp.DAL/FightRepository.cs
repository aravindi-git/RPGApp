using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using RPGApp.DAL.Data;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.DTOs.Fight;
using RPGApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.DAL
{
    public class FightRepository : IFightRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FightRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FightResultDto> Fight(FightRequestDto request)
        {
            FightResultDto fightResult = new FightResultDto();
            try
            {
                var characters = await _context.Characters
                                .Include(c => c.Weapon)
                                .Include(c => c.Skills)
                                .Where(c => request.CharacterIds.Contains(c.Id))
                                .ToListAsync();

                bool defeated = false;
                while (!defeated)
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = String.Empty;

                        bool useWeapon = new Random().Next(2) == 0;

                        if (useWeapon && attacker.Weapon != null)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else if (!useWeapon && attacker.Skills != null)
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }
                        else
                        {
                            fightResult.Log.Add($"{attacker.Name} was not able to attack!");
                            continue;
                        }

                        fightResult.Log
                        .Add($"{attacker.Name} attacked {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            fightResult.Log.Add($"{opponent.Name} has been defeated!");
                            fightResult.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }

                characters.ForEach(c => {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }

            return fightResult;
        }

        public async Task<List<HigherScoreDto>> GetHighScore()
        {
            var higherScore = new List<HigherScoreDto>();
            try
            {
                var characters = await _context.Characters
                                .Where(c => c.Fights > 0)
                                .OrderByDescending(c => c.Victories)
                                .ThenBy(c => c.Defeats)
                                .ToListAsync();

                higherScore = characters.Select(c => _mapper.Map<HigherScoreDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return higherScore;
        }

        public async Task<AttackResultDto> SkillAttack(SkillAttackDto request)
        {
            AttackResultDto attackResult = new AttackResultDto();
            try
            {
                var attacker = await _context.Characters
                              .Include(c => c.Skills)
                              .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                        .Include(c => c.Skills)
                        .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker == null || opponent == null || attacker.Skills == null)
                {
                    throw new Exception("Attacker or Opponent or Attacker's Skills not found.");
                }

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);

                if (skill == null)
                {
                    throw new Exception($"{attacker.Name} does not have the skill.");
                }

                int damage = DoSkillAttack(attacker, opponent, skill);
                string message = string.Empty; 

                if (opponent.HitPoints <= 0)
                {
                    message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                attackResult = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage, 
                    Message = message
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return attackResult;
        }

        public async Task<AttackResultDto> WeaponAttack(WeaponAttackDto request)
        {
            var attackResult = new AttackResultDto();
            try
            {
                var attacker = await _context.Characters
                              .Include(c => c.Weapon)
                              .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                        .Include(c => c.Weapon)
                        .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker == null || opponent == null || attacker.Weapon == null)
                {
                    throw new Exception("Attacker or Opponent or Attacker's Weapon not found.");
                }
                int damage = DoWeaponAttack(attacker, opponent);

                string message = string.Empty; 
                if (opponent.HitPoints <= 0)
                {
                    message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                attackResult = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage, 
                    Message = message
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return attackResult;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            if (attacker.Weapon == null)
            {
                throw new Exception("The attacker has no weapon.");
            }

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        private static int DoSkillAttack(Character attacker, Character opponent, Skill skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }
    }
}
