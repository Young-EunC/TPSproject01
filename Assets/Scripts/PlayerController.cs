using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsControllActive { get; set; } = true;
    private KeyCode _aimKey = KeyCode.Mouse1;

    private PlayerMovement _movement;
    private PlayerStats _stats;
    private Animator _animator;

    [SerializeField] private CinemachineVirtualCamera _aimCam;
    [SerializeField] private CinemachineVirtualCamera _mainCam;

    [SerializeField] private GameObject _aimingUI;
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        SubscrieEvents();
    }
    private void Update()
    {
        HandlePlayerControll();
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }


    private void Init() 
    {
        _stats = GetComponent<PlayerStats>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

    }
    private void HandlePlayerControll() 
    {
        HandleMovement();
        HandleAiming();
    }
    private void HandleMovement()
    {
        Vector3 camDir = _movement.SetAimRotation();
        float moveSpeed = (_stats.IsAiming.Value) ? _stats.WalkSpeed : _stats.RunSpeed;
        Vector3 moveDir = _movement.SetMove(moveSpeed);
        _stats.IsMoving.Value = (moveDir != Vector3.zero);

        Vector3 avatarDir = (_stats.IsAiming.Value) ? camDir : moveDir;
        _movement.SetBodyRotation(avatarDir);

        if (_stats.IsAiming.Value) {
            Vector3 input = _movement.GetInputDirection();
            _animator.SetFloat("X", input.x);
            _animator.SetFloat("Z", input.z);
        }

    }
    private void HandleAiming()
    {
        _stats.IsAiming.Value = Input.GetKey(_aimKey);
        _aimingUI.SetActive(_stats.IsAiming.Value);
    }
    private void SetAimAnimation(bool value) {
        _animator.SetBool("IsAim", value);
    }
    private void SetMoveAnimation(bool value) {
        _animator.SetBool("IsMove", value);
    }
    private void SubscrieEvents()
    {
        _stats.IsMoving.Subscribe(SetMoveAnimation);

        _stats.IsAiming.Subscribe(_aimCam.gameObject.SetActive);
        _stats.IsAiming.Subscribe(SetAimAnimation);
    }
    private void UnsubscribeEvents()
    {
        _stats.IsMoving.Unsubscribe(SetMoveAnimation);

        _stats.IsAiming.Unsubscribe(_aimCam.gameObject.SetActive);
        _stats.IsAiming.Unsubscribe(SetAimAnimation);
    }
}
