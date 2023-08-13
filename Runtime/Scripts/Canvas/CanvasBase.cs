using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SperidMobileFramework.Runtime
{
    public class CanvasBase : MonoBehaviour
    {
        [SerializeField]
        private GameStateManager.GameState _activeState;

        private Dictionary<string, CanvasElement> _elements;

        public static event Action<CanvasBase> OnCanvasAwake;
        public static event Action<CanvasBase> OnCanvasDestroy;

        public GameStateManager.GameState ActiveState { get => _activeState; }

        public void SetActive(GameStateManager.GameState state)
        {
            gameObject.SetActive(_activeState.HasFlag(state));
        }

        protected CanvasElement GetElement(string id)
        {
            return !_elements.ContainsKey(id) ? null : _elements[id];
        }

        protected T GetElementComponent<T>(string id, bool includeChild = false) where T : Component
        {
            if (!_elements.ContainsKey(id))
            {
                return null;
            }
            else
            {
                if (includeChild)
                {
                    var c = _elements[id].gameObject.GetComponent<T>();
                    if (c == null)
                    {
                        c = _elements[id].gameObject.GetComponentInChildren<T>(true);
                    }

                    return c;
                }
                else
                {
                    return _elements[id].gameObject.GetComponent<T>();
                }

            }

        }

        protected virtual void Awake()
        {
            _elements = new Dictionary<string, CanvasElement>();

            foreach (var e in GetComponentsInChildren<CanvasElement>(true))
            {
                Debug.Assert(_elements.ContainsKey(e.Id) == false);
                _elements[e.Id] = e;
            }

            transform.SetParent(CanvasManager.Instance.transform);
            OnCanvasAwake?.Invoke(this);
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void OnDestroy()
        {
            _elements.Clear();
            _elements = null;
            OnCanvasDestroy?.Invoke(this);
        }
    }
}
