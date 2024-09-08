using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.DTOs.Fight
{
    public class FightRequestDto
    {
        public List<int> CharacterIds { get; set; } = new List<int>();
    }
}
