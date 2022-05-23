using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header ("Enemy Stats")]
    [SerializeField] private float _health, _maxHealth = 5f;

    [Header ("Knockback")]
    public Rigidbody2D rb;
    [SerializeField] private float _knockBackForce = 10;
    [SerializeField] private float _knockBackForceUp = 2;
    [SerializeField] private float _knockTime = 2;

    [Header ("Flash Effect")]
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _duration;
    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Coroutine _flashRoutine;

    [Header ("Death Effect")]
    public GameObject DeathEffect;
    public Animator SquashStretchAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        SquashStretchAnimator.SetTrigger("isHit");
        _health -= damageAmount;
        Knockback();
        Flash();

        if(_health <= 0)
        {
            ScoreManager.instance.AddPoint();
            Instantiate(DeathEffect, transform.position, Quaternion.identity); // add point to score
            DestroyEnemy();
        }
    }

    public void Knockback()
    {
        StartCoroutine(knockCo());
        Transform attacker = GetClosestDamageSource();
        Vector2 knockBackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockBackDirection.x, _knockBackForceUp) * _knockBackForce;
    }

    private IEnumerator knockCo()
    {
        // disable movement script so that enemy can be knocked back
        if(rb != null)
        {
            GetComponent<EnemyMovement>().enabled = false;
            yield return new WaitForSeconds(_knockTime);
            GetComponent<EnemyMovement>().enabled = true;
        }
    }

    public Transform GetClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("Weapon");
        float closestDistance = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach(GameObject go in DamageSources)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                currentClosestDamageSource = go.transform;
            }
        }

        return currentClosestDamageSource;
    }

    public void Flash()
    {
        if(_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _flashMaterial;

        yield return new WaitForSeconds(_duration);

        _spriteRenderer.material = _originalMaterial;

        _flashRoutine = null;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
