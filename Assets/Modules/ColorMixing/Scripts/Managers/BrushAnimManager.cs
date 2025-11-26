using System;
using UnityEngine;

//TODO - Connect with the previous one
namespace Modules.ColorMixing.Scripts.Managers {
    public class BrushAnimManager : MonoBehaviour
    {
        private Animator _animator;
        public Action OnAnimationEnd;

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
    }
}