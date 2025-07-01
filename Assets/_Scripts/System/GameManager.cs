
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] InputManager inputManager;
    public StandardCards standard;
    private void Awake()
    {
        inputManager.OnDraw += cardManager.DrawCard;
        inputManager.OnDiscard += cardManager.DiscardHand;
        inputManager.OnPlay += PlayHandle;
    }

    private void PlayHandle()
    {
        //scoreManager.AddSelect();
    }

    private void Start()
    {
        if (standard != null)
        {
            cardManager.Initialize(standard.cards);
        }

    }
}
