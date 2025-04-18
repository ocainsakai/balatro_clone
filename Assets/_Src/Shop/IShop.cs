using ShopSystem.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public interface IShop
    {
        List<IItem> GetItems();
        bool PurchaseItem(GameObject player, string itemName, string currencyType);
    }

}
