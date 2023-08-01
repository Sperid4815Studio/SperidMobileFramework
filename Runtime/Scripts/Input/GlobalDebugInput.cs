using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SperidMobileFramework.Runtime
{
    public static class GlobalDebugInput
    {
        public static Vector2 GetMovementInput()
        {
            if (Keyboard.current == null)
            {
                return Vector2.zero;
            }

            Vector2 result = Vector2.zero;

            var w = Keyboard.current.wKey.isPressed;
            var s = Keyboard.current.sKey.isPressed;
            var a = Keyboard.current.aKey.isPressed;
            var d = Keyboard.current.dKey.isPressed;

            if (w)
            {
                result.y = 1;
            }

            if (s)
            {
                result.y = -1;
            }

            if (a)
            {
                result.x = -1;
            }

            if (d)
            {
                result.x = 1;
            }

            return result;
        }
    }
}
