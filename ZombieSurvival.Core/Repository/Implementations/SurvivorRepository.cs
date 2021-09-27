using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.Models;

namespace ZombieSurvival.Core.Repository.Implementations
{
    public class SurvivorRepository : Repository<Survivor>, ISurvivorRepository
    {
        public SurvivorRepository(DbContext dbContext): base(dbContext)
        {
        }
    }
}
