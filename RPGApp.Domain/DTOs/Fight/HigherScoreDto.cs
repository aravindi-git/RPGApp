using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.DTOs.Fight
{
    public class HigherScoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
