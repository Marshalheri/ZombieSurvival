using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class GetPointsLostResponseDTO
    {
        public long PointsLost { get; set; }
        public long CummulativePoints { get; set; }
    }
}
