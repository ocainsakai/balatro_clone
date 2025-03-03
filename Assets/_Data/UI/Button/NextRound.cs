using UnityEngine;

public class NextRound : UIButton
{

    void Start()
    {
        button.onClick.AddListener(() => GameManager.Instance.ChangePhase(GamePhase.NextBlind));
    }
}
