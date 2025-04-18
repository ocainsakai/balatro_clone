using UnityEngine;

namespace ShopSystem.Item
{
    public interface IItem
    {
        string GetName();
        int GetPrice(string currencyType);
        string GetDescription();
        Sprite GetIcon();
        void OnPurchased(GameObject player);
    }

}
