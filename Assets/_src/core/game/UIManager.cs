using UnityEngine;

namespace Balatro.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject playBtns;
        [SerializeField] GameObject blindPanel;
        [SerializeField] GameObject deck;
        //[SerializeField] GameObject phaseInfo;
        public void HideAll()
        {
            playBtns.SetActive(false);
            deck.SetActive(false);
            blindPanel.SetActive(false);
        }
        public void ShowBlindUI()
        {
            HideAll();
            blindPanel.SetActive(true);
        }
        public void ShowPlayUI()
        {
            HideAll();
            playBtns.SetActive(true);
            deck.SetActive(true );
        }
    }

}
