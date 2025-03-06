using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DeckSelectionUI : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    
    private DeckSO currentDeckSelection => DeckManager.Instance.GetCurrentDeck();
    [SerializeField] private GameObject lockedOverlayPrefab;

    private void OnEnable()
    {

        leftBtn.onClick.RemoveAllListeners();
        rightBtn.onClick.RemoveAllListeners();

        leftBtn.onClick.AddListener(UndoDeck);
        rightBtn.onClick.AddListener(NextDeck);

        UpdateSelection();
    }
    private void NextDeck()
    {
        DeckManager.Instance.Next();
        UpdateSelection();
    }
    private void UndoDeck()
    {
        DeckManager.Instance.Undo();

        UpdateSelection();

    }
    private void UpdateSelection()
    {
        bool isUnlocked = DeckManager.Instance.IsCurrentDeckUnlock();
        lockedOverlayPrefab.SetActive(!isUnlocked);
        art.sprite = currentDeckSelection.cardBackImage;
    }
    //private void PopulateDeckList()
    //{
    //    // Xóa tất cả deck cũ
    //    foreach (Transform child in deckContainer)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // Lấy danh sách tất cả deck
    //    List<DeckSO> allDecks = DeckManager.Instance.GetAllDecks();

    //    // Tạo UI cho mỗi deck
    //    foreach (DeckSO deck in allDecks)
    //    {
    //        GameObject buttonObj = Instantiate(deckButtonPrefab, deckContainer);
    //        Button button = buttonObj.GetComponent<Button>();

    //        // Setup thông tin hiển thị
    //        Image deckImage = buttonObj.transform.Find("DeckImage").GetComponent<Image>();
    //        TextMeshProUGUI deckName = buttonObj.transform.Find("DeckName").GetComponent<TextMeshProUGUI>();

    //        deckImage.sprite = deck.Icon;
    //        deckName.text = deck.Name;

    //        // Kiểm tra trạng thái mở khóa
    //        bool isUnlocked = DeckManager.Instance.IsDeckUnlocked(deck.Id);

    //        if (isUnlocked)
    //        {
    //            // Thêm listener cho deck đã mở khóa
    //            button.onClick.AddListener(() => {
    //                DeckManager.Instance.SetCurrentDeck(deck.Id);
    //                // Highlight deck được chọn
    //                UpdateSelection(deck.Id);
    //            });
    //        }
    //        else
    //        {
    //            // Hiển thị overlay khóa
    //            GameObject lockedOverlay = Instantiate(lockedOverlayPrefab, buttonObj.transform);
    //            TextMeshProUGUI unlockText = lockedOverlay.transform.Find("UnlockCondition").GetComponent<TextMeshProUGUI>();
    //            unlockText.text = deck.unlockCondition;

    //            // Disable button hoặc thêm tooltip
    //            button.interactable = false;
    //        }

    //        // Highlight deck hiện tại
    //        if (DeckManager.Instance.GetCurrentDeck().Id == deck.Id)
    //        {
    //            buttonObj.transform.Find("SelectedIndicator").gameObject.SetActive(true);
    //        }
    //    }
    //}


}