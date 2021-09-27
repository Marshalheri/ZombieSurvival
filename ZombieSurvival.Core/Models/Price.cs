using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Models
{
    public class Price : BaseModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Point { get; set; }
    }
}
