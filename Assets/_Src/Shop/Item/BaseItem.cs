using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem.Item
{
    public abstract class BaseItem : ScriptableObject, IItem
    {
        [SerializeField] protected string _name;
        [SerializeField] protected Dictionary<string, int> prices; 
        [SerializeField] protected string description;
        [SerializeField] protected Sprite icon;

        public string GetName() => name;
        public int GetPrice(string currencyType) => prices.ContainsKey(currencyType) ? prices[currencyType] : 0;
        public string GetDescription() => description;
        public Sprite GetIcon() => icon;
        public abstract void OnPurchased(GameObject player);
    }
}

