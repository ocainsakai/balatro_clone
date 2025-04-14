# Internal API Design

## GridSystem

```csharp
// Add card to grid
void AddCard(Card card, int position);

// Validate current hand
PokerHand EvaluateHand(List<Card> cards);

// Event triggered when hand is resolved
event Action<PokerHand> OnHandResolved;
