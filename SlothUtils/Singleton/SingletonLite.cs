using System;

namespace SlothUtils
{
    public abstract class SingletonLite<T> where T : new()
    {
        private static T sInstance;

        public static T Instance
        {
            get
            {
                if (SingletonLite<T>.sInstance == null)
                {
                    SingletonLite<T>.sInstance = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
                }
                return SingletonLite<T>.sInstance;
            }
        }

        public static bool Exists
        {
            get
            {
                return SingletonLite<T>.sInstance != null;
            }
        }

        public static void Ensure()
        {

        }
    }
}
