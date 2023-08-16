using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SperidMobileFramework.Runtime
{
    public class CameraBase : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _targetOffset;

        [SerializeField]
        private float _smoothTime;

        [SerializeField]
        private GameObject _followTarget;

        protected Vector3 _velocity = Vector3.zero;

        public Vector3 TargetOffset { get => _targetOffset; protected set { _targetOffset = value; } }

        public float SmoothTime { get => _smoothTime; }

        public GameObject FollowTarget { get => _followTarget;}

        public Vector3 Velocity { get => _velocity;}

        public virtual void SetFollowTarget(GameObject target)
        {
            _followTarget = target;
        }

        protected virtual  void Start()
        {
        
        }

        protected virtual void Update()
        {
        
        }
    }
}
