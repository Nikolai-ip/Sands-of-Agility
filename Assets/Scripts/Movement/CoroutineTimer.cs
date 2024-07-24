using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public class CoroutineTimer : MonoBehaviour
    {
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSec;
        private Action _onTimeStart;
        private Action _onTimeStopped;

        [SerializeField] private float _time = 1f;

        public void Init(Action onTimeStart, Action onTimeStopped)
        {
            _onTimeStart = onTimeStart;
            _onTimeStopped = onTimeStopped;
        }
        
        private void Awake()
        {
            _waitForSec = new WaitForSeconds(_time);
        }
        
        private void OnValidate()
        {
            _waitForSec = new WaitForSeconds(_time);
        }

        public void StartTimer()
        {
            _onTimeStart();

            _coroutine = StartCoroutine(ChangeCoyoteJumpAfterTimer());
        }

        public void StopTimer()
        {
            if (_coroutine == null)
                return;
            
            StopCoroutine(_coroutine);
            _onTimeStopped();
        }

        private IEnumerator ChangeCoyoteJumpAfterTimer()
        {
            yield return _waitForSec;
            _onTimeStopped();
        }
    }
}