using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    public bool IsControllActive { get; set; } = true;
    private KeyCode _aimKey = KeyCode.Mouse1;
    private KeyCode _shootKey = KeyCode.Mouse0;

    private PlayerMovement _movement;
    private PlayerStats _stats;
    private Animator _animator;
    [SerializeField]  private Animator _aimAnimator;

    [SerializeField] private CinemachineVirtualCamera _aimCam;
    [SerializeField] private CinemachineVirtualCamera _mainCam;

    [SerializeField] private HpBarUI _hpUI;

    [SerializeField] private GameObject _aimingUI;

    [SerializeField] private GunController _weapon;
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
        HandleShooting();
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
    private void HandleShooting() {
        if (_stats.IsAiming.Value && Input.GetKey(_shootKey))
        {
            _stats.IsAttacking.Value = _weapon.Shoot();
        }
        else 
        {
            _stats.IsAttacking.Value = false;
        }
    }

    private void SetHpUIGuage(int curHp) {
        float hp = curHp / _stats.MaxHp;
        _hpUI.SetImageFillAmount(hp);
    }

    public void TakeDamaged(int value) {
        _stats.CurrentHp.Value -= value;
        if (_stats.CurrentHp.Value <= 0) { Died(); }
    }
    public void TakeHealed(int value) {
        int hp = _stats.CurrentHp.Value + value;
        _stats.CurrentHp.Value = (int)Mathf.Clamp(hp, 0, _stats.MaxHp);
    }
    public void Died() {
        Debug.Log("»ç¸Á!");
    }

    private void SetAimAnimation(bool value) {
        _animator.SetBool("IsAim", value);
        _aimAnimator.SetBool("IsAim", value);
    }
    private void SetMoveAnimation(bool value) {
        _animator.SetBool("IsMove", value);
    }
    private void SetAttackingAnimation(bool value) {
        _animator.SetBool("IsShoot", value);
    }
    private void SubscrieEvents()
    {
        _stats.CurrentHp.Subscribe(SetHpUIGuage);

        _stats.IsMoving.Subscribe(SetMoveAnimation);

        _stats.IsAiming.Subscribe(_aimCam.gameObject.SetActive);
        _stats.IsAiming.Subscribe(SetAimAnimation);

        _stats.IsAttacking.Subscribe(SetAttackingAnimation);
    }
    private void UnsubscribeEvents()
    {
        _stats.CurrentHp.Unsubscribe(SetHpUIGuage);

        _stats.IsMoving.Unsubscribe(SetMoveAnimation);

        _stats.IsAiming.Unsubscribe(_aimCam.gameObject.SetActive);
        _stats.IsAiming.Unsubscribe(SetAimAnimation);

        _stats.IsAttacking.Unsubscribe(SetAttackingAnimation);
    }
}
