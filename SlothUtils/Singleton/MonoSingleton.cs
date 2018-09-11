
using UnityEngine;

namespace SlothUtils
{
    /// <summary>
    /// DDOL singleton.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T _Instance = null;

        public static T Instance
        {
            get
            {
                if (null == _Instance)
                {
                    GameObject go = GameObject.Find("Eternal");
                    if (null == go)
                    {
                        go = new GameObject("Eternal");
                        DontDestroyOnLoad(go);
                    }
                    _Instance = go.AddComponent<T>();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Raises the application quit event.
        /// </summary>
        private void OnApplicationQuit()
        {
            _Instance = null;
        }

    }
}




