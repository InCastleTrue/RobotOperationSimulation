using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Player player;
    public GameObject prefabToSpawn;
    public AudioSource audioSource;
    public AudioClip HitSound;
    public float prefabLifetime = 0.5f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("ItemBox"))
        {
            Enemy1 enemy = other.gameObject.GetComponent<Enemy1>();


            if (enemy != null)
            {
                if (prefabToSpawn != null)
                {
                    audioSource.PlayOneShot(HitSound);
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, enemy.transform.position, Quaternion.identity);
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
                enemy.TakeDamage(player.damage);
                gameObject.transform.position = new Vector3(0, 500, 0);
            }
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                
                if (prefabToSpawn != null)
                {
                    audioSource.PlayOneShot(HitSound);
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, boss.transform.position, Quaternion.identity);
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
                boss.TakeDamage(player.damage);
                gameObject.transform.position = new Vector3(0, 500, 0);

            }
            ItemBox itemBox = other.gameObject.GetComponent<ItemBox>();
            if (itemBox != null)
            {
                
                if (prefabToSpawn != null)
                {
                    audioSource.PlayOneShot(HitSound);
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, itemBox.transform.position, Quaternion.identity);
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
                itemBox.TakeDamage(player.damage);
                gameObject.transform.position = new Vector3(0, 500, 0);
            }

        }else
        {
            Destroy(gameObject, 0.5f);
        }



    }
    IEnumerator DestroyPrefabAfterDelay(GameObject prefabToDestroy)
    {
        yield return new WaitForSeconds(prefabLifetime);
        Destroy(prefabToDestroy);
        Destroy(gameObject);
    }
}
