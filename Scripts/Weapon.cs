using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header ("Object Components")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firepoint;
    [SerializeField] private Animator _squashStretchAnimator;

    [Header ("Audio Components")]
    [SerializeField] private AudioSource _shootSoundEffect;

    [Header ("FireRate Values")]
    [SerializeField] public float FireRate;
    [SerializeField] private float _fireForce;
    [SerializeField] private float _upgradeFireRateAmount = .05f;
    private float nextFire = 0f;

    void Awake()
    {
        FireRate = 0.4f;
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            _squashStretchAnimator.SetTrigger("Shoot");
            nextFire = Time.time + FireRate;
            GameObject projectile = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(_firepoint.up * _fireForce, ForceMode2D.Impulse);
            _shootSoundEffect.Play();
        }
    }

    public void IncreaseRateOfFire()
    {
        if(FireRate > .05)
        {
            FireRate -= _upgradeFireRateAmount;
        }
    }
}
