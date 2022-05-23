using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header ("Grabbed Objects")]
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _weapon;
    
    [Header ("Audio Properties")]
    [SerializeField] private AudioSource _selectedSoundEffect;

    [Header ("Upgrade Values")]
    [SerializeField] private float _upgradeAmountSpeed = 2f;
    [SerializeField] private float _upgradeAmountHealth = 25f;

    void Start()
    {
        _upgradeMenu.SetActive(false);
    }

    public void activateMenu()
    {
        _upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void removeMenu()
    {
        _selectedSoundEffect.Play();
        _upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void upQuickness()
    {
        _player.GetComponent<PlayerMovement>()._moveSpeed += _upgradeAmountSpeed;
        removeMenu();
    }

    public void upFireRate()
    {
        _weapon.GetComponent<Weapon>().IncreaseRateOfFire();
        removeMenu();
    }

    public void upSturdy()
    {
        _player.GetComponent<PlayerHealth>().MaxHealth += _upgradeAmountHealth;
        _player.GetComponent<PlayerHealth>().maxOutHealth();
        removeMenu();
    }
}
