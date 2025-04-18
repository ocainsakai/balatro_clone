using ShopSystem.Currency;
using ShopSystem.Item;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShopSystem {
    public class Shop : MonoBehaviour, IShop
    {
        [SerializeField] private List<IItem> items = new List<IItem>();
        [SerializeField] private Sprite defaultIcon;

        public UnityEvent<string, IItem> OnItemPurchased = new UnityEvent<string, IItem>();
        public List<IItem> GetItems()
        {
            return items;
        }

        public bool PurchaseItem(GameObject player, string itemName, string currencyType)
        {
            foreach (var item in items)
            {
                if (item.GetName() == itemName)
                {
                    var currency = player.GetComponent<ICurrency>();
                    if (currency != null && currency.SpendCurrency(currencyType, item.GetPrice(currencyType)))
                    {
                        item.OnPurchased(player);
                        OnItemPurchased.Invoke(itemName, item);
                        return true;
                    }
                    return false;
                }
            }
            Debug.Log($"Item {itemName} not found.");
            return false;
        }
    }
}


