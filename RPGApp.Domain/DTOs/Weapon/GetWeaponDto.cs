using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGApp.Domain.DTOs.Weapon
{
    public class GetWeaponDto
    {
        public string Name { get; set; } = String.Empty;
        public int Damage { get; set; }
    }
}
