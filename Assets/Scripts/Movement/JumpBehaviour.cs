using Common;
using UnityEngine;

namespace Movement
{
    public class JumpBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private bool _isOnGround;
        private float _jumpForce;

        [SerializeField] private GroundChecker _groundChecker;

        public float _metersToJump;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _groundChecker.OnLayerStateChanged += UpdateIsOnGround;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _groundChecker.IsLayerStaying) 
                Jump();
        }

        private void OnDisable() => _groundChecker.OnLayerStateChanged += UpdateIsOnGround;

        private void OnValidate() => UpdateJumpForce();

        private void Jump()
        {
            if (_isOnGround)
                _rb.AddForce(_jumpForce * Vector2.up);
        }
        
        private void UpdateIsOnGround() => _isOnGround = _groundChecker.IsLayerStaying;
        private void UpdateJumpForce() => _jumpForce = _metersToJump * Config.JumpForceToMeters;
    }
}
