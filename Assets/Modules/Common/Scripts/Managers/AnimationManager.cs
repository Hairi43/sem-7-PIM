using System;
using UnityEngine;

namespace Modules.Common.Scripts.Managers
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator _animator;
        public Action OnAnimationEnd;
        public Action OnAnimationReturn;

        private void Start()
        {
            _animator =  GetComponent<Animator>();
        }

        public void PlayAnimation(string animationName)
        {
            _animator.Play(animationName,0,0f);
        }
        

        public void OnAnimationDone()
        {
            OnAnimationEnd?.Invoke();
        }
        public void OnAnimationReturning()
        {
            OnAnimationReturn?.Invoke();
        }
    }
}
