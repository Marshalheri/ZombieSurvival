using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.Repository
{
    public interface IUnitOfWork
    {
        IInventoryRepository InventoryRepository { get; }
        ISurvivorRepository SurvivorRepository { get; }
        IPriceRepository PriceRepository { get; }
        Task BeginTransactionAsync();
        Task SaveAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
