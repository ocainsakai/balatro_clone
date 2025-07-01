using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    private Factory<Card, CardData> CardFactory;
    //private Factory<JokerCard, JokerCardData> JokerFactory = new();
    [SerializeField] Transform cardPrefab;
    [SerializeField] List<JokerCardData> start;
    private void Awake()
    {
        CardFactory = new Factory<Card, CardData>(cardPrefab);
    }
    public Card CreateCard(CardData card, Sprite cardBack, Transform parent = null)
    {
        //Debug.Log(CardFactory);
        //Debug.Log(CardFactory.prefab);
        var newCard = CardFactory.Create(card, parent);
        newCard.transform.name = card.CardName;
        newCard.Initialize(card, cardBack);
        return newCard;
    }
    public class Factory<T, TData>
    {
        public Factory(Transform prefeb)
        {
            this.prefab = prefeb;
        }
        public static Dictionary<T, Transform> items = new Dictionary<T, Transform>();
        public Transform prefab;
        public T Create(TData data, Transform parent = null)
        {
            Transform newItem = Instantiate(prefab, parent);
            var item = newItem.GetComponent<T>();
            items.Add(item, newItem);
            return item;
        }
        public Transform GetItem(T item)
        {
            return items.TryGetValue(item, out var cardObj) ? cardObj : null;
        }
    }
}
