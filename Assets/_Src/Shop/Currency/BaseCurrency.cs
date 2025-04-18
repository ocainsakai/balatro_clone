using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem.Currency
{
    public class BaseCurrency : MonoBehaviour, ICurrency
    {
        private Dictionary<string, int> currencies = new Dictionary<string, int>();
        public int GetCurrency(string currencyType)
        {
            return currencies.ContainsKey(currencyType) ? currencies[currencyType] : 0;
        }

        public bool SpendCurrency(string currencyType, int amount)
        {
            if (currencies.ContainsKey(currencyType) && currencies[currencyType] >= amount)
            {
                currencies[currencyType] -= amount;
                Debug.Log($"Spent {amount} {currencyType}. Remaining: {currencies[currencyType]}");
                return true;
            }
            Debug.Log($"Not enough {currencyType} to spend {amount}.");
            return false;
        }
        public void AddCurrencyType(string currencyType, int initialAmount)
        {
            if (!currencies.ContainsKey(currencyType))
            {
                currencies[currencyType] = initialAmount;
            }
        }

    }

}
