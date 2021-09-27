using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvival.Core.DTOs
{
    public class TradeRequestDTO
    {
        public string BuyerUsername { get; set; }
        public List<TradeItem> BuyerTradeItems { get; set; }
        public string SellerUsername { get; set; }
        public List<TradeItem> SellerTradeItems { get; set; }

        public bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;

            if (string.IsNullOrEmpty(BuyerUsername))
            {
                problemSource = "BuyerUsername";
                return false;
            }
            if (BuyerTradeItems.Count < 1)
            {
                problemSource = "BuyerTradeItems";
                return false;
            }
            foreach (var item in BuyerTradeItems)
            {
                if (!item.IsValid(out problemSource))
                {
                    problemSource = $"Buyer {problemSource}";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(SellerUsername))
            {
                problemSource = "SellerUsername";
                return false;
            }
            if (SellerTradeItems.Count < 1)
            {
                problemSource = "SellerTradeItems";
                return false;
            }
            foreach (var item in SellerTradeItems)
            {
                if (!item.IsValid(out problemSource))
                {
                    problemSource = $"Seller {problemSource}";
                    return false;
                }
            }
            if (BuyerUsername.Equals(SellerUsername))
            {
                problemSource = $"{BuyerUsername} and {SellerUsername} is the same person";
                return false;
            }

            return true;
        }
    }

    public class TradeItem
    {
        public int Quantity { get; set; }
        public InventoryItems Item { get; set; }

        public bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;
            if (Quantity < 1)
            {
                problemSource = "Item Quantity";
                return false;
            }

            return true;
        }
    }
}
