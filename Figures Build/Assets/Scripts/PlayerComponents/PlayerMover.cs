using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private PlayerInput _playerInput;
        private float _deadZoneValue = 0.1f;
        private float _rotateValue = 90;

        private Vector2 _direction;
        private Vector2 _rotate;
        private Vector2 _rotation;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }
        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void Update()
        {
            _rotate = _playerInput.Player.Look.ReadValue<Vector2>();
            _direction = _playerInput.Player.Move.ReadValue<Vector2>();

            Look(_rotate);
            Move(_direction);
        }
  
        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < _deadZoneValue)
                return;

            float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

            Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            transform.position += move * scaledMoveSpeed;
        }

        private void Look(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < _deadZoneValue)
                return;

            float scaledLookSpeed = _rotateSpeed * Time.deltaTime;
            _rotation.y += rotate.x * scaledLookSpeed;
            _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledLookSpeed, -_rotateValue, _rotateValue);
            transform.localEulerAngles = _rotation;
        }
    }
}