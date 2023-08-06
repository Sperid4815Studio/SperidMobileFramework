using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SperidMobileFramework.Runtime
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                // Try find instance.
                _instance = FindObjectOfType<T>();

                // Create new instance.
                if (_instance == null)
                {
                    var ob = new GameObject("SingletonBehaviour");
                    _instance = ob.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance);

                return _instance;
            }
        }

        public static bool IsAlive()
        {
            return _instance != null;
        }

        public bool IsInitialize { get; protected set; }

        public virtual void Initialize()
        {
            IsInitialize = true;
        }

        public virtual void Destroy()
        {
            IsInitialize = false;
            Destroy(this);
        }

    }
}
