using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float MaxHealth;
    public float CurrentHealth;
    [SerializeField] private HealthBar _healthBar;

    [Header ("Particle Effects")]
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _deathText;
    [SerializeField] private GameObject _hurtEffect;

    [Header ("IFrames")]
    [SerializeField] private float _iFramesDuration;
    [SerializeField] private int _numberOfFlashes;
    [SerializeField] private SpriteRenderer _spriteRend;

    [Header ("Sound Effects")]
    [SerializeField] private AudioSource _damagedSoundEffect;
    [SerializeField] private AudioSource _reviveTickSoundEffect;
    [SerializeField] private AudioSource _fullReviveSoundEffect;

    void Awake()
    {
        _healthBar.SetHealth(MaxHealth, MaxHealth);
        Physics2D.IgnoreLayerCollision(6,7,false);
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        _healthBar.SetHealth(MaxHealth, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        Instantiate(_hurtEffect, transform.position, Quaternion.identity);
        CurrentHealth-=amount;
        _healthBar.SetHealth(CurrentHealth, MaxHealth);
        _damagedSoundEffect.Play();
        if(CurrentHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
        // After taking damage, give invulnerability
        StartCoroutine(Invulnerability());
    }

    private IEnumerator Invulnerability()
    {
        if(CurrentHealth > 0)
        {
        Physics2D.IgnoreLayerCollision(6,7,true); // First int is player layer, second is enemy
        for(int i = 0; i < _numberOfFlashes; i++)
        {
            _spriteRend.color = new Color(.8f, .8f, .8f, .8f); // R, G, B, ALPHA
            yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
            _reviveTickSoundEffect.Play();
            _spriteRend.color = new Color(.5f, .5f, .5f, .5f); // R, G, B, ALPHA
            yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
        }
        // -- INVULNERABILITY OVER --
        _spriteRend.color = Color.white; // reset to default colors
        _fullReviveSoundEffect.Play();
        Physics2D.IgnoreLayerCollision(6,7,false);
        } else {
            // ; This means player is at or less than 0 health, disable everything and turn red
            Physics2D.IgnoreLayerCollision(6,7,true);
            _spriteRend.color = new Color(1f, 0f, 0f, 1f);
        }
    }

    private IEnumerator GameOver()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Instantiate(_deathText, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }

    public void maxOutHealth()
    {
        CurrentHealth = MaxHealth;
        _healthBar.SetHealth(MaxHealth, MaxHealth); //-- called twice because for some reason that's only when it updates!
        _healthBar.SetHealth(MaxHealth, MaxHealth);
    }
}
