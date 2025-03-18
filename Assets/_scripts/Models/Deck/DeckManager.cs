using UnityEngine;
using System.Collections.Generic;


public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    public SpriteRenderer image => GetComponent<SpriteRenderer>();

    [SerializeField] private DeckCollectionSO deckCollection 
        => Resources.Load<DeckCollectionSO>("Collections/DeckCollectionSO");

    // Theo dõi trạng thái hiện tại
    public int DeckCount => deckCollection.avalableItems.Count;
    private DeckSO currentDeck;
    private HashSet<string> unlockedIds = new HashSet<string>();


    public List<CardSO> playingDeck = new List<CardSO>();
    public List<CardSO> choosingCards = new List<CardSO>();
    public List<CardSO> handCards = new List<CardSO>();
    public List<CardSO> playedCards = new List<CardSO>();
    public List<CardSO> jokers = new List<CardSO>();
    // Lưu trạng thái player
    [System.Serializable]
    private class PlayerDeckData
    {
        public string currentId;
        public List<string> unlockedIds = new List<string>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            LoadPlayerDeckData();
            Debug.Log(Instance == null);
    }

    private void LoadPlayerDeckData()
    {
        // Ban đầu chỉ có starting deck được mở khóa
        if (PlayerPrefs.HasKey("DeckData"))
        {
            string json = PlayerPrefs.GetString("DeckData");
            PlayerDeckData data = JsonUtility.FromJson<PlayerDeckData>(json);

            // Thiết lập deck đã mở khóa
            unlockedIds = new HashSet<string>(data.unlockedIds);
            // Thiết lập deck hiện tại
            currentDeck = deckCollection.avalableItems.Find(d => d.Id == data.currentId);
            if (currentDeck == null && deckCollection.avalableItems.Count > 0)
            {
                currentDeck = deckCollection.startingDeck;
            }
        }
        else
        {
            // Mở khóa deck mặc định
            currentDeck = deckCollection.startingDeck;
            unlockedIds.Add(currentDeck.Id);
            //SavePlayerDeckData();
        }


    }


    public bool IsCurrentDeckUnlock()
    {
        return unlockedIds.Contains(currentDeck.Id);

    }
    

    public DeckSO GetCurrentDeck()
    {
        return currentDeck;
    }
    public void Next()
    {
        int index = deckCollection.avalableItems.IndexOf(currentDeck);
        index++;
        if (index >= DeckCount)
        {
            index = 0;
        }
        currentDeck = deckCollection.avalableItems[index];
    }
    public void Undo()
    {
        int index = deckCollection.avalableItems.IndexOf(currentDeck);
        index--;
        if (index < 0)
        {
            index = DeckCount - 1;
        }
        currentDeck = deckCollection.avalableItems[index];
    }
    public bool SetCurrentDeck()
    {
        // Chỉ có thể chọn deck đã mở khóa
        if (unlockedIds.Contains(currentDeck.Id))
        {
            //SavePlayerDeckData();
            return true;
        }
        return false;
    }

    public void UnlockDeck(string Id)
    {
        DeckSO deckToUnlock = deckCollection.avalableItems.Find(d => d.Id == Id);
        if (deckToUnlock != null && !unlockedIds.Contains(Id))
        {
            unlockedIds.Add(Id);
            //SavePlayerDeckData();

            // Thông báo mở khóa (có thể thêm event system ở đây)
            Debug.Log($"Unlocked new deck: {deckToUnlock.Name}");
        }
    }


    private void InitializePlayingDeck()
    {
        playingDeck = new List<CardSO>(currentDeck.defaultCards);
    }

    public void Show()
    {
        this.image.enabled = true;
    }
    public void Hide()
    {
        this.image.enabled = false;
    }
}