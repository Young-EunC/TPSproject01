using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField][field: Range(0, 10)]
    public float WalkSpeed { get; set; }
    [field: SerializeField][field: Range(0, 10)]
    public float RunSpeed { get; set; }
    [field: SerializeField][field: Range(0, 10)]
    public float RotateSpeed { get; set; }

    [field: SerializeField]
    [field: Range(0, 10)]
    public float MaxHp { get; set; }

    public ObservableObject<int> CurrentHp { get; private set; } = new();

    public ObservableObject<bool> IsAiming { get; private set; } = new();
    public ObservableObject<bool> IsMoving { get; private set; } = new();
    public ObservableObject<bool> IsAttacking { get; private set; } = new();

}
