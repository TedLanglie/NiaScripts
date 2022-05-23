using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _collideDamage;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // If collided with Player, get the PlayerHealth script and call their
    // -- Take Damage function and pass the damage amount.
    // -- Then destroy this object.
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            var healthComponent = other.gameObject.GetComponent<PlayerHealth>();
            healthComponent.TakeDamage(_collideDamage);
            Destroy(gameObject);
        }
        
    }
}
