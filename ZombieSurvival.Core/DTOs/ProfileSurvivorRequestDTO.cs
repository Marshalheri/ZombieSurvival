using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class ProfileSurvivorRequestDTO
    {
        public string Firstname { get; set; }
        public int Age { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public Location Location { get; set; }
        public List<SurvivorInventory> SurvivorInventories { get; set; }

        public bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;

            if (Age <= 0)
            {
                problemSource = "Age";
                return false;
            }
            if (string.IsNullOrEmpty(Firstname))
            {
                problemSource = "Firstname";
                return false;
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                problemSource = "Lastname";
                return false;
            }
            if (string.IsNullOrEmpty(Username))
            {
                problemSource = "Username";
                return false;
            }
            if (!SurvivorInventories.Any())
            {
                problemSource = "SurvivorInventories";
                return false;
            }

            return true;
        }
    }

    public class Location
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }

    public class SurvivorInventory
    {
        public int Point { get; set; }
        public InventoryItems InventoryItem { get; set; }
    }
}
