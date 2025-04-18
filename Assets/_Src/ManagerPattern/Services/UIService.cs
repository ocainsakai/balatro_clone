using DG.Tweening;
using UnityEngine;

namespace Game.Services
{
    public class UIService
    {
        private readonly GameObject mainMenuUI;
        public void ShowMainMenu()
        {
            mainMenuUI.SetActive(true);
            mainMenuUI.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }

        public void HideMainMenu()
        {
            mainMenuUI.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack)
                .OnComplete(() => mainMenuUI.SetActive(false));
        }
    }
}

