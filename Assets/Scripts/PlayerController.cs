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

    [SerializeField] private CinemachineVirtualCamera _aimCam;
    [SerializeField] private CinemachineVirtualCamera _mainCam;

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

    }
    private void SubscrieEvents()
    {
        _stats.IsAiming.Subscribe(_aimCam.gameObject.SetActive);
    }
    private void UnsubscribeEvents()
    {
        _stats.IsAiming.Unsubscribe(_aimCam.gameObject.SetActive);
    }
    private void HandlePlayerControll() 
    {
        _stats.IsAiming.Value = Input.GetKey(_aimKey);
        HandleMovement();
        HandleAiming();
    }
    private void HandleMovement()
    {
        Vector3 camRotateDir = _movement.SetAimRotation();
        float moveSpeed;
        if (_stats.IsAiming.Value) { moveSpeed = _stats.WalkSpeed; }
        else { moveSpeed = _stats.RunSpeed; }
        Vector3 moveDir = _movement.SetMove(moveSpeed);
        _stats.IsMoving.Value = (moveDir != Vector3.zero);

    }
    private void HandleAiming()
    {

    }
}
