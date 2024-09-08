using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.DTOs.Fight
{
    public class AttackResultDto
    {
        public string Attacker { get; set; } = String.Empty;
        public string Opponent { get; set; } = String.Empty;
        public int AttackerHP { get; set; }
        public int OpponentHP { get; set; }
        public int Damage { get; set; }
        public string Message { get; set; }
    }
}
