using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Blind
{
    public class UIBlindPanel : MonoBehaviour
    {

        [SerializeField] private Transform blindPrf;
        [SerializeField] private List<BlindDataSO> blindDatas;
        public void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                Transform newBlind = Instantiate(blindPrf);
                newBlind.SetParent(this.transform);
                newBlind.localScale = Vector3.one;
                newBlind.GetComponent<UIBlind>().UpdateBlindInfo(blindDatas[i]);
            }
        }
    }

}
