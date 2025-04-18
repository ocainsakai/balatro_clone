using UnityEngine;

namespace Game.Managers
{
    public interface IManager
    {
        void Initialize();
        void ResetManager();
        void Cleanup();
    }
}

