using Joker;
using Shop;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] IntVariable money;
    [SerializeField] List<JokerSO> jokers;
    [SerializeField] UI_Shop ui_Shop;
    List<Item> existItems = new List<Item>();
    public Item RandomItem()
    {
        var valids = jokers.Except(existItems).ToList();
        var item = valids[Random.Range(0, valids.Count())];
        return item;
    }
    public void ShowShop()
    {
        var item1 = ui_Shop.CreateItem(RandomItem());
        item1.buy = OnBuy;
        var item2 = ui_Shop.CreateItem(RandomItem());
        item2.buy = OnBuy;
    }
    public void OnBuy(Item item)
    {
        if (money.Value < item.Price) return;
        money.Value -= item.Price;
    }
}
