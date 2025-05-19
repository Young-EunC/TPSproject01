using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] LayerMask _targetLayer;
    [SerializeField][Range(0, 100)] private float _attackRange;
    [SerializeField] private int _shootDamage;
    [SerializeField] private float _shootDelay;
    [SerializeField] private AudioClip _shootSFX;
    private Camera _camera;
    private CinemachineImpulseSource _impulseSource;
    private bool _canShoot { get => _currentTimer <= 0; }
    private float _currentTimer;


    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        HandleCanShoot();
    }

    private void Init() {
        _camera = Camera.main;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public bool Shoot() {
        if (!_canShoot) { return false; }
        PlayShootingSFX();
        PlayCameraFX();
        PlayShootingSideFX();
        _currentTimer = _shootDelay;

        IDamagable target = RayShoot();
        if(target == null) { return true; }
        target.TakeDamaged(_shootDamage);
        return true;
    }

    private void HandleCanShoot() {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward* _attackRange, Color.green);
        if (_canShoot) { return; }
        _currentTimer -= Time.deltaTime;
    }

    private IDamagable RayShoot() {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _attackRange, _targetLayer))
        {
            return hit.transform.gameObject.GetComponent<IDamagable>();
        }
        return null;
    }

    private void PlayShootingSFX() {
        SFXController sfx = GameManager.Instance.Audio.GetSFX();
        sfx.Play(_shootSFX);
    }
    private void PlayCameraFX() {
        _impulseSource.GenerateImpulse();
    }
    private void PlayShootingSideFX() { 
    
    }
}
