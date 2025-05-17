using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
namespace Game.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardArt cardArt;
        public float OffsetY => cardArt._spriteRenderer.bounds.size.y;
        public Card GetCard() { return cardArt._card; }
        public void SetData(Card card)
        {
            cardArt.SetCard(card);  
        }
        public async Task OnDiscard()
        {
            transform.parent = null;
            await transform.DOMove(new Vector3(10, 0,0), 0.2f).AsyncWaitForCompletion();
            Destroy(gameObject);
        }



        public static CardView CreateCard(Card card, Transform parent)
        {
            var cardPrefab = Resources.Load<CardView>("_Prefabs/CardViewPrefab");
            var cardView = Instantiate(cardPrefab, parent.position, Quaternion.identity, parent);
            cardView.SetData(card);
            _cardViewDict[card.CardID] = cardView;
            return cardView;
        }
        public static Dictionary<SerializableGuid, CardView> _cardViewDict = new Dictionary<SerializableGuid, CardView>();
        public static CardView GetCardView(SerializableGuid cardId)
        {
            return _cardViewDict.TryGetValue(cardId, out CardView result) ? result : null;
        }
    }
}