using UnityEngine;

namespace ScreenManagers
{
    public abstract class ScreenManager : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
    }
}