using UnityEngine;


namespace Balatro.Blind
{
    public enum BlindType
    {
        Small,
        Big,
        Boss
    }

    [CreateAssetMenu(fileName = "BlindData", menuName = "Scriptable Objects/BlindData")]
    public class BlindDataSO : ScriptableObject
    {
        [Header("Blind Identification")]
        public string blindName;
        public BlindType blindType;
        public Sprite blindIcon;
        [Header("Difficulty Modifiers")]
        //public int requiredScore;
        public int minAnte;
        public int reward;
        public float scoreMultiplier = 1f;

        [Header("Special Effects")]
        public bool disableHandValue;
        public bool randomizeCardValues;
        public int maxHandSize;

        [TextArea(3, 10)]
        public string blindDescription;

#if UNITY_EDITOR
        private void GetData()
        {
            this.name = name;
            var token = this.name?.Split(' ');
            blindIcon = token?.Length >= 2 ? Resources.Load<Sprite>($"PNG/{token[0]}_{token[1]}") : null;

        }
        protected void OnValidate()
        {
            GetData();
        }
#endif
        //// Áp dụng hiệu ứng Blind
        //public void ApplyBlindEffect(HandManager handManager)
        //{
        //    // Các hiệu ứng đặc biệt của Blind
        //    if (disableHandValue)
        //    {
        //        // Vô hiệu hóa giá trị của các lá bài
        //    }

        //    if (randomizeCardValues)
        //    {
        //        // Làm ngẫu nhiên giá trị của các lá bài
        //    }

        //    if (maxHandSize > 0)
        //    {
        //        handManager.MAX_HAND_SIZE = maxHandSize;
        //    }
        //}
    }
}
