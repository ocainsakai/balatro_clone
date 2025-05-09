using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
namespace Game.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardArt cardArt;
        public Card GetCard() { return cardArt._card; }
        public void SetData(Card card)
        {
            cardArt.SetCard(card);  
        }
        public async Task OnDiscard()
        {
            await transform.DOScale(0, 0.2f).AsyncWaitForCompletion();

            transform.parent = null;
            Destroy(gameObject);
        }
    }
}