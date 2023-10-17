using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    public float activationDistance = 30f;
    private bool portalActive = false;
    private GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        InvokeRepeating("CheckDistanceToEnemies", 0f, 1f);
    }

    private void CheckDistanceToEnemies()
    {
        bool isEnemiesExistence = true;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < activationDistance)
                {
                    isEnemiesExistence = false;
                    break;
                }
            }
        }
        if (isEnemiesExistence || enemies.Length == 0)
        {
            portalActive = true;
        }
        else
        {
            portalActive = false;
        }

        gameObject.SetActive(portalActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (portalActive && other.CompareTag("Player"))
        {
            other.transform.position = destination.position;
        }
    }
}