using Common;
using UnityEngine;

namespace Movement
{
    public class JumpBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private bool _isOnGround = false;
        private bool _isCoyoteJump = false;
        private bool _isJumped = false;
        [SerializeField] private float _jumpForce;

        [SerializeField] private CoroutineTimer _timer;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private float _metersToJump;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _timer.Init(() => { _isCoyoteJump = true; }, () => { _isCoyoteJump = false; });
            
            _groundChecker.OnLayerStateChanged += UpdateIsOnGround;
            _groundChecker.OnLayerEntered += _timer.StopTimer;
            _groundChecker.OnLayerExited += _timer.StartTimer;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && CanJump())
            {
                Jump();
                _isJumped = true;
            }
        }

        private void OnDisable()
        {
            _groundChecker.OnLayerStateChanged -= UpdateIsOnGround;
            _groundChecker.OnLayerEntered -= _timer.StopTimer;
            _groundChecker.OnLayerExited -= _timer.StartTimer;
        }

        private void OnValidate()
        {
            // UpdateJumpForce();
        }

        private void Jump()
        {
            var jumpForce = _jumpForce;
            if (_rb.velocity.y < 0)
                jumpForce -= _rb.velocity.y;
            
            _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }

        private bool CanJump() => !_isJumped && (_isOnGround || _isCoyoteJump);

        private void UpdateIsOnGround()
        {
            _isOnGround = _groundChecker.IsLayerStaying;
            if (_isOnGround)
                _isJumped = false;
        }

        private void UpdateJumpForce() => _jumpForce = _metersToJump * Config.JumpForceToMeters;
    }
}
