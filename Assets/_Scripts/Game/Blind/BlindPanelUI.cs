using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
public class BlindPanelUI : MonoBehaviour
{
    [SerializeField] protected UIDocument document;
    [SerializeField] protected StyleSheet styleSheet;
    protected VisualElement root;
    protected VisualElement container;
    BlindViewModel[] blinds = new BlindViewModel[3];

    private class BlindUI
    {
        public VisualElement blindContainer;
        public Button selectBtn;
        public Image blindIcon;
        public Label blindNameTxt;
        public Label blindScore;
        public Label blindReward;
        public IEnumerator CreateBlindUI(VisualElement container)
        {
            blindContainer = container.CreateChild("Blind Container");
            selectBtn = blindContainer.CreateChild<Button>("Selector");
            selectBtn.text = "Select";
            blindNameTxt = blindContainer.CreateChild<Label>("Blind Name");
            blindNameTxt.text = "Blind";
            blindIcon = blindContainer.CreateChild<Image>("Blind Icon");

            blindScore = blindContainer.CreateChild<Label>("Score At Least");
            blindScore.text = "300";
            blindReward = blindContainer.CreateChild<Label>("Reward");
            blindReward.text = "+$$$";
            yield return null;
        }
    }
    private void Awake()
    {
        StartCoroutine(Initialize());
    }
    protected virtual IEnumerator Initialize()
    {
        root = document.rootVisualElement;
        var small = new BlindUI();
        var big = new BlindUI();
        var boss = new BlindUI();
        yield return small.CreateBlindUI(root);
        yield return big.CreateBlindUI(root);
        yield return boss.CreateBlindUI(root);
    }
    
}
