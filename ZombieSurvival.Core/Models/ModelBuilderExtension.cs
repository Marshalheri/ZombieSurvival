using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvival.Core.DTOs;

namespace ZombieSurvival.Core.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>().HasData(
                new Price
                {
                    Id = 1,
                    Name = InventoryItems.Water.ToString(),
                    Point = 4,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                },
                new Price
                {
                    Id = 2,
                    Name = InventoryItems.Food.ToString(),
                    Point = 3,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                },
                new Price
                {
                    Id = 3,
                    Name = InventoryItems.Medication.ToString(),
                    Point = 2,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                },
                new Price
                {
                    Id = 4,
                    Name = InventoryItems.Ammunition.ToString(),
                    Point = 1,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                });
        }
    }
}

