using UnityEngine;
using TMPro;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Shop {
    public class ItemDisplay : MonoBehaviour, IPointerClickHandler, IItem
    {
        [SerializeField] TextMeshProUGUI price;
        [SerializeField] Image image;
        public Action<IItem> buy;
        private IItem data;
        public int Price => data.Price;

        public string Name => data.Name;

        public Sprite sprite => data.sprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            buy?.Invoke(this);
        }

        public void SetInit(IItem item)
        {
            this.data = item;
            this.price.text = $"${item.Price}";
            this.image.sprite = item.sprite;
        }
    }
}


