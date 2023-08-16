using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SperidMobileFramework.Runtime
{
    public class TopDownCamera : CameraBase
    {
        public override void SetFollowTarget(GameObject target)
        {
            base.SetFollowTarget(target);
            transform.position = FollowTarget.transform.position;
            transform.position += TargetOffset;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            if (FollowTarget != null)
            {
                var pos = FollowTarget.transform.position + TargetOffset; ;
                transform.position = Vector3.SmoothDamp(transform.position,pos,ref _velocity, SmoothTime);
            }
        }
    }
}
