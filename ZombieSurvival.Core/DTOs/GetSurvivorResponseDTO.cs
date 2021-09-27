using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class GetSurvivorResponseDTO
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int ContaminationCount { get; set; }
        public List<SurvivorInventoryDTO> Inventory { get; set; }
    }

    public class SurvivorInventoryDTO
    {
        public string Name { get; set; }
        public int Point { get; set; }
    }
}
