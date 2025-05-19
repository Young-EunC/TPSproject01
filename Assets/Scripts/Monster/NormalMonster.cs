using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMonster : Monster, IDamagable
{
    private bool _canControl;
    private bool _canTracking = true;
    [SerializeField] private int _maxHp;
    private ObservableObject<int> CurHp = new();
    private ObservableObject<bool> isMoving = new();
    private ObservableObject<bool> isAttacking = new();
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _targetTransform;

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        HandleControl();
    }

    private void Init() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void HandleControl() {
        if (!_canControl) { return; }
        HandleMove();
    }

    private void HandleMove() {
        if (_targetTransform == null) { return; }
        if (_canTracking)
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
            isMoving.Value = true;
        }
        else 
        {
            isMoving.Value = false;
        }
    }
    public void TakeDamaged(int value) 
    {
        
    }
}
