
using UnityEngine;
using UnityEngine.UI;

namespace Shop {
    public class UI_Shop : MonoBehaviour
    {
        [SerializeField] Transform itemPrf;
        [SerializeField] Transform itemContainer;
        [SerializeField] Button NextRound;
        [SerializeField] GameStateEvent gameStateEvent;
        private void OnEnable()
        {
            NextRound.onClick.AddListener(() => { gameStateEvent.Raise(Core.GameState.Blinding); });
        }
        private void OnDisable()
        {
            NextRound.onClick.RemoveAllListeners();
        }
        public ItemDisplay CreateItem(Item item)
        {
            var newItem = Instantiate(itemPrf);
            var display = newItem.GetComponent<ItemDisplay>();
            newItem.SetParent(itemContainer);
            display.SetInit(item);
            return display;
        }
    }
}

