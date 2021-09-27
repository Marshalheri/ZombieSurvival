using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Models
{
    public class Inventory : BaseModel
    {
        public string Name { get; set; }
        public int Point { get; set; }
        public bool IsActive { get; set; }
        public long SurvivorId { get; set; }

        public virtual Survivor Survivor { get; set; }
    }
}
