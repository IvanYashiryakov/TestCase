using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _stopSpeed = 1;
    [SerializeField] private Vector2 _limitVelocity;
    [SerializeField] private Transform _visualModelTransform;

    private Rigidbody2D _rigidbody2D;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private Vector2 _targetSpeed;
    private float _visualModelRotationSpeed = 15f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _horizontalSpeed = Input.GetAxisRaw("Horizontal") * _speed;
        _verticalSpeed = Input.GetAxisRaw("Vertical") * _speed;

        Move();
    }

    private void Move()
    {
        _targetSpeed = new Vector2(_horizontalSpeed * Time.fixedDeltaTime, _verticalSpeed * Time.fixedDeltaTime);

        if (_targetSpeed != Vector2.zero)
        {
            _rigidbody2D.velocity += _targetSpeed;
            LimitVelocity(_limitVelocity);

            var angle = Quaternion.Euler(new Vector3(0, 0, -Vector2.SignedAngle(_rigidbody2D.velocity, Vector2.up)));
            _visualModelTransform.rotation = Quaternion.RotateTowards(_visualModelTransform.rotation, angle, _visualModelRotationSpeed);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.MoveTowards(_rigidbody2D.velocity, Vector2.zero, _stopSpeed);
        }
    }

    private void LimitVelocity(Vector2 limit)
    {
        if (_rigidbody2D.velocity.magnitude > limit.magnitude)
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, limit.magnitude);
    }
}
