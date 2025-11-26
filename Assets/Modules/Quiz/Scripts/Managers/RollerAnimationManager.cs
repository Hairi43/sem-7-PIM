using System;
using UnityEngine;

namespace Modules.Quiz.Scripts.Managers {
    public class RollerAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        public Action OnAnimationReturn;

        private void Start()
        {
            _animator =  GetComponent<Animator>();
        }

        public void PlayAnimation(string animationName)
        {
            _animator.Play(animationName,0,0f);
        }

        public void OnAnimationReturning()
        {
            OnAnimationReturn?.Invoke();
        }
    }
}
