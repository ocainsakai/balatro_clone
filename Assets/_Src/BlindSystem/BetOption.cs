using UnityEngine;

[CreateAssetMenu(menuName = "Game/Bet Option")]
public class BetOption : ScriptableObject
{
    public string betName;
    public int requiredScore;

    private void OnValidate()
    {
        if (this.name != null)
        {
            this.betName = TxtUltil.AddSpacesToSentence(name);

        }
    }
    //public Reward reward;
    //public List<Modifier> modifiers;
}
