using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float spawnInterval = 10.0f;
    private GameObject currentEnemy;
    private GameObject currentHealthBar;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemy == null)
            {
                int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
                Vector3 spawnPosition = spawnPoint.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                currentEnemy = Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);

                // Find and enable the health bar and indicator bar within the monster
                Transform healthBarTransform = currentEnemy.transform.Find("HealthBar"); // Adjust the name as needed
                if (healthBarTransform != null)
                {
                    currentHealthBar = healthBarTransform.gameObject;
                    currentHealthBar.SetActive(true);
                }

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1.0f);

            if (!currentEnemy)
            {
                // Disable the health bar and indicator bar within the monster
                if (currentHealthBar != null)
                {
                    currentHealthBar.SetActive(false);
                }

                yield return new WaitForSeconds(spawnInterval - 1.0f);
            }
        }
    }
}
