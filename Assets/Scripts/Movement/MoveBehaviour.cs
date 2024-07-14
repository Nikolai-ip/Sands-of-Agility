using Common.Extensions;
using UnityEngine;

namespace Movement
{
    public class MoveBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rb;

        [SerializeField] private float _maxMoveSpeed;
        [SerializeField] private float _velocityPower;
        [SerializeField] private AnimationCurve _acceleration;
        [SerializeField] private AnimationCurve _deselaration;

        private void Start() => _rb = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            float targetSpeed = Input.GetAxis("Horizontal") * _maxMoveSpeed;
            float speedDif = targetSpeed - _rb.velocity.x;
            float normalizeSpeedDif = Mathf.Abs(targetSpeed - speedDif) / _maxMoveSpeed;
            float accelerate = targetSpeed.EqualsTo(0f)
                ? _deselaration.Evaluate(normalizeSpeedDif)
                : _acceleration.Evaluate(normalizeSpeedDif);

            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelerate, _velocityPower) * Mathf.Sign(speedDif);
            _rb.AddForce(movement * Vector2.right);
        }
    }
}
