using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float SpawnRate = 1f;

    [SerializeField] private GameObject[] EnemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(SpawnRate);

        while(canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, EnemyPrefabs.Length);
            GameObject EnemyToSpawn = EnemyPrefabs[rand];

            Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
        }
    }
    void Update()
    {
        
    }
}
