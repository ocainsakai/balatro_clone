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
        [SerializeField] private TextMeshPro text;
        public Card GetCard() { return cardArt._card; }
        public void SetData(Card card)
        {
            cardArt.SetCard(card);  
        }
        public void ShowValue()
        {
            text.gameObject.SetActive(true);
            text.text = $"+{cardArt._card.Value}";
            text.transform.DOLocalMoveY(1.5f, 0.2f);
        }
        public async Task OnDiscard()
        {
            await transform.DOScale(0, 0.2f).AsyncWaitForCompletion();

            transform.parent = null;
            Destroy(gameObject);
        }
    }
}