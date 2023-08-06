using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SperidMobileFramework.Runtime
{
    public class AssetEntity
    {
        public int RefCount { get; set; }
        public AsyncOperationHandle Handle { get; set; }

        public bool IsLoading => Handle.IsDone == false;
    }
}
