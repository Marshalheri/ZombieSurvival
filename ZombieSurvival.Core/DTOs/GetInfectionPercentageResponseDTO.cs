using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class GetInfectionPercentageResponseDTO
    {
        public string InfectedPercentage { get; set; }
        public string NonInfectedPercentage { get; set; }
    }
}
