using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject prefabToSpawn; // 생성할 프리팹
    public float prefabLifetime = 0.1f; // 프리팹의 수명 (초)

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("ItemBox"))
        {
            Enemy1 enemy = other.GetComponent<Enemy1>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);

                // 적의 위치에 프리팹을 생성합니다.
                if (prefabToSpawn != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, enemy.transform.position, Quaternion.identity);

                    // 일정 시간 후에 프리팹을 파괴합니다.
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
            }
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(1);

                // 적의 위치에 프리팹을 생성합니다.
                if (prefabToSpawn != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, boss.transform.position, Quaternion.identity);

                    // 일정 시간 후에 프리팹을 파괴합니다.
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
            }
            ItemBox itemBox = other.GetComponent<ItemBox>();
            if (itemBox != null)
            {
                itemBox.TakeDamage(1);


                if (prefabToSpawn != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, itemBox.transform.position, Quaternion.identity);


                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
            }

        }
       StartCoroutine(DestroyParticleDelay());
    }

    IEnumerator DestroyPrefabAfterDelay(GameObject prefabToDestroy)
    {
        yield return new WaitForSeconds(prefabLifetime);
        Destroy(prefabToDestroy);

    }
    IEnumerator DestroyParticleDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}


/*    void OnParticleCollision(GameObject other)

    {

        Debug.Log("파티클 충돌");

    }*/


