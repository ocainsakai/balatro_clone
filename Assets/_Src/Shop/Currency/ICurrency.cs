using UnityEngine;

namespace ShopSystem.Currency {
    public interface ICurrency
    {
        bool SpendCurrency(string currencyType, int amount);
        int GetCurrency(string currencyType);
    }
}
