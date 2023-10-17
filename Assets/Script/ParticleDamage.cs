using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject prefabToSpawn; // ������ ������
    public float prefabLifetime = 0.1f; // �������� ���� (��)

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("ItemBox"))
        {
            Enemy1 enemy = other.GetComponent<Enemy1>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);

                // ���� ��ġ�� �������� �����մϴ�.
                if (prefabToSpawn != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, enemy.transform.position, Quaternion.identity);

                    // ���� �ð� �Ŀ� �������� �ı��մϴ�.
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
            }
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(1);

                // ���� ��ġ�� �������� �����մϴ�.
                if (prefabToSpawn != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, boss.transform.position, Quaternion.identity);

                    // ���� �ð� �Ŀ� �������� �ı��մϴ�.
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

        Debug.Log("��ƼŬ �浹");

    }*/


