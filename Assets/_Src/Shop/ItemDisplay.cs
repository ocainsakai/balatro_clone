using UnityEngine;
using TMPro;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Shop {
    public class ItemDisplay : MonoBehaviour, IPointerClickHandler, Item
    {
        [SerializeField] TextMeshProUGUI price;
        [SerializeField] Image image;
        public Action<Item> buy;
        private Item data;
        public int Price => data.Price;

        public string Name => data.Name;

        public Sprite sprite => data.sprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            buy?.Invoke(this);
        }

        public void SetInit(Item item)
        {
            this.data = item;
            this.price.text = $"${item.Price}";
            this.image.sprite = item.sprite;
        }
    }
}


