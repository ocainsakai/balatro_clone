using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] PokerContext pokerContext;
    [SerializeField] ScoreContext scoreContext;
    Label pokerName;
    Label pokerChip;
    Label pokerMult;
    Label roundScore;
    Label targetScore;
    UIDocument document;
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        // poker region
        pokerName = document.rootVisualElement.Query<Label>("PokerName");
        pokerChip = document.rootVisualElement.Query<Label>("PokerChip");
        pokerMult = document.rootVisualElement.Query<Label>("PokerMult");

        pokerName.Subcribe<PokerHandType>(pokerContext.HandType, StrExtensionType.WithSpace);
        pokerChip.Subcribe<int>(pokerContext.Chip);
        pokerMult.Subcribe<int>(pokerContext.Mult);
       

        // score region
        roundScore = document.rootVisualElement.Query<Label>("ScoreText");
        targetScore = document.rootVisualElement.Query<Label>("StatTxt");
        targetScore.Subcribe<int>(scoreContext.targetScore);
        roundScore.Subcribe<int>(scoreContext.RoundScore);
       
    }

}
