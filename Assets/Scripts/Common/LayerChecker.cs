using System;
using UnityEngine;

namespace Common
{
    public class LayerChecker : MonoBehaviour
    {
        public event Action OnLayerEntered;
        public event Action OnLayerExited;
        public event Action OnLayerStateChanged;

        private bool _isLayerStaying;
        
        protected int _checkingLayer;

        public bool IsLayerStaying
        {
            get => _isLayerStaying;
            private set
            {
                _isLayerStaying = value;
                OnLayerStateChanged?.Invoke();
            }
        }

        private void Awake() => SetLayer();
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == _checkingLayer)
            {
                IsLayerStaying = true;
                OnLayerEntered?.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer == _checkingLayer)
            {
                IsLayerStaying = false;
                OnLayerExited?.Invoke();
            }
        }

        protected virtual void SetLayer() { }
    }
}