using Game.Pokers;
using TMPro;
using UnityEngine;
using UniRx;
using VContainer;
using System.Collections;
using DG.Tweening;
using UnityEngine.UIElements;
public class PokerView : MonoBehaviour
{

    [Inject] PokerViewModel viewModel;
    private UIDocument document;

    private Label pokerName;
    private Label pokerChip;
    private Label pokerMult;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        pokerName = document.rootVisualElement.Query<Label>("PokerName");
        pokerChip = document.rootVisualElement.Query<Label>("ChipText");
        pokerMult = document.rootVisualElement.Query<Label>("MultText");

        viewModel.Data.Subscribe(x => {pokerName.text = x.Name; });
        viewModel.Chip.Subscribe(x => {pokerChip.text = x.ToString(); });
        viewModel.Mult.Subscribe(x => { pokerMult.text = x.ToString();});
    }
}
