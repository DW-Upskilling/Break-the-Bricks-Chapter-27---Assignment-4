using UnityEngine;

namespace BreakTheBricks2D.GenericClass
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                Init();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        protected abstract void Init();
    }

    public abstract class GlobalSingleton<T> : MonoBehaviour where T : GlobalSingleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                Init();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        protected abstract void Init();
    }
}
