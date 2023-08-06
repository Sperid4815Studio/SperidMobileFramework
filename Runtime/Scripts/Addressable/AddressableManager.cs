using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SperidMobileFramework.Runtime
{
    public class AddressableManager : SingletonBehaviour<AddressableManager>
    {
        private Dictionary<object, AssetEntity> _assetEntities; 

        public override void Initialize()
        {
            name = "AddressableManager";
            _assetEntities = new Dictionary<object, AssetEntity>();
            base.Initialize();
        }

        public override void Destroy()
        {
            var array = _assetEntities.Keys.ToArray();
            foreach (var t in array)
            {
                Release(t);
            }

            _assetEntities.Clear();
            _assetEntities = null;
        }

        public void LoadAssetSync<TObject>(object key) where TObject : Object
        {
            if (_assetEntities.ContainsKey(key))
            {
                _assetEntities[key].RefCount++;
                return;
            }

            var op = Addressables.LoadAssetAsync<TObject>(key);

            // WaitForCompletion = Sync wait
            op.WaitForCompletion();

            _assetEntities[key] = new AssetEntity()
            {
                RefCount = 1,
                Handle = op
            };
        }

        public IEnumerator LoadAssetAsync<TObject>(object key) where TObject : Object
        {
            if (_assetEntities.ContainsKey(key))
            {
                _assetEntities[key].RefCount++;
                yield break;
            }

            var op = Addressables.LoadAssetAsync<TObject>(key);

            //Wait until load complete
            while (!op.IsDone)
            {
                yield return null;
            }

            _assetEntities[key] = new AssetEntity()
            {
                RefCount = 1,
                Handle = op
            };
        }

        public async void LoadAssetAsync<TObject>(object key, System.Action<object> onComplete) where TObject : Object
        {
            if (_assetEntities.ContainsKey(key))
            {
                _assetEntities[key].RefCount++;
                return;
            }

            var op = Addressables.LoadAssetAsync<TObject>(key);

            await op.Task;

            _assetEntities[key] = new AssetEntity()
            {
                RefCount = 1,
                Handle = op
            };

            onComplete?.Invoke(key);
        }

        public void Release(object key, bool notifyError = true)
        {
            if (_assetEntities != null)
            {
                if (_assetEntities.ContainsKey(key))
                {
                    _assetEntities[key].RefCount--;
                    if (_assetEntities[key].RefCount <= 0)
                    {
                        Addressables.Release(_assetEntities[key].Handle);
                        _assetEntities.Remove(key);
                    }
                }
                else if (notifyError)
                {
                    Debug.LogError($"AddressableManager : {key} not found,but try release.");
                }
            }
        }

        public TObject Instantiate<TObject>(object key) where TObject : Object
        {
            if (_assetEntities.ContainsKey(key))
            {
                return GameObject.Instantiate(_assetEntities[key].Handle.Result as TObject);
            }
            else
            {
                Debug.LogError($"AddressableManager : {key} not loaded,but try instantiate.");
                return null;
            }
        }

        public GameObject Instantiate(object key, Vector3 position, Quaternion rotation)
        {
            if (_assetEntities.ContainsKey(key))
            {
                return GameObject.Instantiate(_assetEntities[key].Handle.Result as GameObject, position, rotation);
            }
            else
            {
                Debug.LogError($"AddressableManager : {key} not loaded,but try instantiate.");
                return null;
            }
        }

        public bool IsAssetLoaded(object key)
        {
            if (_assetEntities.ContainsKey(key))
            {
                return _assetEntities[key].IsLoading == false;
            }
            else
            {
                return false;
            }
        }

        public bool IsAssetLoading(object key)
        {
            return _assetEntities.ContainsKey(key) && _assetEntities[key].IsLoading;
        }

        private void Update()
        {

        }
    }
}
