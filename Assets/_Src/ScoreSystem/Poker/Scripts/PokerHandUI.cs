using UnityEngine;

public class PokerHandUI : MonoBehaviour
{
    [SerializeField] private PokerHandInfoPanel panel;
    [SerializeField] private PokerHandData nonePokerHand;
    [SerializeField] private PokerHandDatabase database;
    void Start()
    {
        panel.Show(nonePokerHand);
    }

    public void Show(PokerHand poker)
    {
        Debug.Log(poker);
        var data = database.GetData(poker);
        Debug.Log(data);
        panel.Show(data);
    }
}