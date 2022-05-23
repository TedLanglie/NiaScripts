using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject _impactEffect;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
            Impact();
            break;
            case "Enemy":
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(3);
            Impact();
            break;
        }
    }

    public void Impact()
    {
        Instantiate(_impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
