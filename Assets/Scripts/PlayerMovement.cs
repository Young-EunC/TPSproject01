using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _avatar;
    [SerializeField] private Transform _aim;

    private Rigidbody _rigid;
    private PlayerStats _playerStats;

    [field: SerializeField][field: Range(-90, 0)] private float _minPitch;
    [field: SerializeField][field: Range(0, 90)] private float _maxPitch;
    [field: SerializeField][field: Range(0, 5)] private float _mouseSensitivity;

    private Vector2 _currentRotation;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rigid = GetComponent<Rigidbody>();
        _playerStats = GetComponent<PlayerStats>();
    }

    public Vector3 SetMove(float moveSpeed) {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = _rigid.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        _rigid.velocity = velocity;

        return moveDirection;
    }
    public Vector3 SetAimRotation() {
        Vector2 mouseDir = GetMouseDirection();
        _currentRotation.x += mouseDir.x;
        _currentRotation.y = Mathf.Clamp(_currentRotation.y + mouseDir.y, _minPitch, _maxPitch);

        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);
        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(_currentRotation.y, currentEuler.y, currentEuler.z);
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;
    }

    public void SetBodyRotation() { }

    private Vector2 GetMouseDirection() {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;
        return new Vector2(mouseX, mouseY);
    }
    public Vector3 GetMoveDirection() {
        Vector3 input = GetInputDirection();
        Vector3 direction = (transform.right * input.x) + (transform.forward * input.z);
        return direction.normalized;
    }
    private Vector3 GetInputDirection() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        return new Vector3(x, 0, z);
    }
}
