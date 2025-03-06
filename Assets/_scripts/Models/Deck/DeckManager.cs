using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [SerializeField] private DeckCollectionSO deckCollection 
        => Resources.Load<DeckCollectionSO>("Collections/DeckCollectionSO");

    // Theo dõi trạng thái hiện tại
    private DeckSO currentDeck;
    private HashSet<string> unlockedIds = new HashSet<string>();
    public int DeckCount => deckCollection.avalableItems.Count;
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
            return;
        }

        LoadPlayerDeckData();
        //Debug.Log(deckCollection.avalableItems.Count);
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
            SavePlayerDeckData();
        }
        //foreach (var item in deckCollection.avalableItems)
        //{
        //    if (unlockedIds.Contains(item.Id))
        //    {
        //        item.Unlock();
        //    } else
        //    {
        //        item.Lock();
        //        Debug.Log(item.name);
        //    }
        //}

    }

    private void SavePlayerDeckData()
    {
        PlayerDeckData data = new PlayerDeckData
        {
            currentId = currentDeck.Id,
            unlockedIds = unlockedIds.ToList()
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("DeckData", json);
        PlayerPrefs.Save();
    }

    public List<DeckSO> GetAllDecks()
    {
        return deckCollection.avalableItems;
    }

    public List<DeckSO> GetUnlockedDecks()
    {
        return deckCollection.avalableItems.Where(deck => unlockedIds.Contains(deck.Id)).ToList();
    }
    public bool IsCurrentDeckUnlock()
    {
        return unlockedIds.Contains(currentDeck.Id);

    }
    public bool IsDeckUnlocked(string Id)
    {
        return unlockedIds.Contains(Id);
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
    public void SetCurrentDeck(string Id)
    {
        // Chỉ có thể chọn deck đã mở khóa
        if (unlockedIds.Contains(Id))
        {
            DeckSO newDeck = deckCollection.avalableItems.Find(d => d.Id == Id);
            if (newDeck != null)
            {
                currentDeck = newDeck;
                SavePlayerDeckData();
            }
        }
    }

    public void UnlockDeck(string Id)
    {
        DeckSO deckToUnlock = deckCollection.avalableItems.Find(d => d.Id == Id);
        if (deckToUnlock != null && !unlockedIds.Contains(Id))
        {
            unlockedIds.Add(Id);
            deckToUnlock.Unlock();
            SavePlayerDeckData();

            // Thông báo mở khóa (có thể thêm event system ở đây)
            Debug.Log($"Unlocked new deck: {deckToUnlock.Name}");
        }
    }
}