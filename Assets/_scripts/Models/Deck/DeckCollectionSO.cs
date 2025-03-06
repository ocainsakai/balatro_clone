using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "DeckCollection", menuName = "Balatro/DeckCollectionSO")]
public class DeckCollectionSO : CollectionSO<DeckSO>
{
    
    public DeckSO startingDeck;
   
}