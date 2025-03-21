using UnityEngine;

public class UIBlindManager : MonoBehaviour
{
    [SerializeField] private UIBlind small;
    [SerializeField] private UIBlind big;
    [SerializeField] private UIBlind boss;

    public void SetBlind(Blind blind, int baseChips, int index = 0, int status = 0)
    {
        switch (index)
        {
            case 0:
                small.Initlize(blind, baseChips);
                break;
            case 1:
                big.Initlize(blind, baseChips);
                break;
            case 2:
                boss.Initlize(blind, baseChips);
                break;
        }
    }
}
