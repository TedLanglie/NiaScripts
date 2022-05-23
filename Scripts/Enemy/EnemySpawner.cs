using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header ("Rates")]
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _spawnRateMultiplier;
    
    [Header ("Spawnlings")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _spawnEffect;
    void Awake()
    {
        _spawnTimer = 1; // we have this for when spawners are duplicated!
    }

    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(CloneSpawner());
    }

    IEnumerator EnemySpawn()
    {
        while(true)
        {
            Vector3 enemyspawn = new Vector3(Random.Range(-120f,120f), Random.Range(-55f,55f), 0f); // x,y,z
            Instantiate(_spawnEffect, enemyspawn, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Instantiate(_enemy, enemyspawn, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTimer);
            _spawnTimer = _spawnTimer / _spawnRateMultiplier;
        }
    }

    IEnumerator CloneSpawner()
    {
        yield return new WaitForSeconds(65);
        Instantiate(gameObject);
    }
}
