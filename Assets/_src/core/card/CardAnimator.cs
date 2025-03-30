using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Cards
{
    public class CardAnimator : MonoBehaviour
    {
        [SerializeField] Transform handPosition;
        [SerializeField] Transform deckPosition;

        public IEnumerator CalculateCard(List<Card> cards, Action<int> onChipChange, Action<int> OnMulChange, Action onComplete)
        {

            foreach (var item in cards)
            {
                onChipChange?.Invoke(item.data.value);
                yield return item.ShowValue();
            }
            onComplete?.Invoke();
        }
        public IEnumerator FillToHand(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                StartCoroutine(card.MoveToPos(handPosition.position + Vector3.right * 80f * i, 0.3f));
            }
            yield return new WaitForSeconds(0.3f);
        }

        public IEnumerator MoveCardsToDeck(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                card.isSelect = false;
                StartCoroutine(card.MoveToPos(deckPosition.position, 0.2f, false));
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}

