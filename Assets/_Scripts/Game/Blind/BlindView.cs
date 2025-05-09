using UnityEngine;
using UnityEngine.UIElements;
using UniRx;
using VContainer;

public class BlindView : MonoBehaviour
{
    [Inject] BlindManager manager;
    UIDocument document;

    Label blindName;
    Image blindIcon;
    Label blindDescription;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        blindName = document.rootVisualElement.Query<Label>("BlindName");
        VisualElement x = document.rootVisualElement.Query<VisualElement>("BlindIcon");
        blindIcon = x.CreateChild<Image>("icon");
        blindIcon.style.alignItems = Align.Center;
        blindDescription = document.rootVisualElement.Query<Label>("StatTxt");

        manager.CurrentBlind.Subscribe(blind => {
            blindName.text = blind.Data.Name;
            blindIcon.sprite = blind.Data.Artwork;
            blindDescription.text = $"Score at least\n " +
                                    $"{manager.targetScore} \n" +
                                    $" to reward: {blind.Data.Reward}$";
        });
    }
}
