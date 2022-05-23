using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] float _moveSpeed = 10f;
    private Vector2 _moveDirection;
    private Transform _target;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        TakeDirection();
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    private void TakeDirection()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        _moveDirection = direction;
    }

    private void MoveEnemy()
    {
        rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _moveSpeed;
    }
}
