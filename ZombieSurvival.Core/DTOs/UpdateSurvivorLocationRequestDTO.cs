using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class UpdateSurvivorLocationRequestDTO
    {
        public string Username { get; set; }
        public Location Location { get; set; }

        public bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;

            if (string.IsNullOrEmpty(Username))
            {
                problemSource = "Username";
                return false;
            }

            return true;
        }
    }
}
