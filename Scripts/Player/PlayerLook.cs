using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private Camera _sceneCamera;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 _mousePosition;

    void Update()
    {
        _mousePosition = _sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 aimDirection = _mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
