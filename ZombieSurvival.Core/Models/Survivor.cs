using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Models
{
    public class Survivor : BaseModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int ContaminationCount { get; set; }
        public bool IsInfected { get; set; }
        public virtual IList<Inventory> Inventories { get; set; }
    }
}
