using UnityEngine;

namespace SperidMobileFramework.Runtime
{
    public static class GameObjectUtility
    {
        public static void SetActiveIfNeed(this GameObject gameObject, bool value)
        {
            if (gameObject.activeSelf != value)
            {
                gameObject.SetActive(value);
            }
        }

        public static void CustomSetActive(this GameObject gameObject, bool value)
        {
            if (value)
            {
                gameObject.transform.localScale = Vector3.one;
            }
            else
            {
                gameObject.transform.localScale = Vector3.zero;
            }
        }
    }
}
