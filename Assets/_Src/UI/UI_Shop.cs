
using UnityEngine;

namespace Shop {
    public class UI_Shop : MonoBehaviour
    {
        [SerializeField] Transform itemPrf;
        [SerializeField] Transform itemContainer;

        public ItemDisplay CreateItem(IItem item)
        {
            var newItem = Instantiate(itemPrf);
            var display = newItem.GetComponent<ItemDisplay>();
            newItem.SetParent(itemContainer);
            display.SetInit(item);
            return display;
        }
    }
}

