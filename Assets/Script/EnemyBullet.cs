using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public AudioSource audioSource;
    public AudioClip bombSound;
    public GameObject prefabToSpawn;
    public float prefabLifetime = 0.5f;
    public Transform target;
    private Player player;
    public float someThresholdDistance = 50.0f;
    private bool isChasing = false;
    private float moveSpeed = 5.0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < someThresholdDistance)
        {
            isChasing = true;
            Vector3 targetPosition = player.transform.position;
            transform.LookAt(targetPosition);
            float move = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);
        }
        else
        {
            isChasing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (prefabToSpawn != null)
                {
                    audioSource.PlayOneShot(bombSound);
                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, player.transform.position, Quaternion.identity);
                    StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
                }
                player.HitDamage(damage); // 보스 데미지로 수정해야 함
                gameObject.transform.position = new Vector3(0, 500, 0);
            }
        }
    }

    IEnumerator DestroyPrefabAfterDelay(GameObject prefabToDestroy)
    {
        yield return new WaitForSeconds(prefabLifetime);
        Destroy(prefabToDestroy);
        Destroy(gameObject);
    }
}
