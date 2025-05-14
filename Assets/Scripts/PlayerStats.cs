using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField][field: Range(0, 10)]
    public float WalkSpeed { get; set; }
    //[field: SerializeField][field: Range(0, 10)]
    public float RunSpeed { get; set; }
    //[field: SerializeField][field: Range(0, 10)]
    public float RotateSpeed { get; set; }

    public ObservableObject<bool> isAiming { get; private set; } = new();
    public ObservableObject<bool> isMoving { get; private set; } = new();
    public ObservableObject<bool> isAttacking { get; private set; } = new();

}
