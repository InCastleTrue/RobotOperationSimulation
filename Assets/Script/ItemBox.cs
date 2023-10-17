using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    public int boxHp;
    public GameObject[] itemPrefab;

    private Player player;

    public GameObject breakParticle;

    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip breakSound;
    
    public BoxCollider boxCollider;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        boxHp--;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<Player>();
    }

    public void TakeDamage(int damage)
    {
        
        if (boxHp <= 0) {

            Destroy(boxCollider);
            audioSource.PlayOneShot(breakSound);
            GameObject spawnedPrefab = Instantiate(breakParticle, gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyParticlefterDelay());
            StartCoroutine(DestroyPrefabAfterDelay(spawnedPrefab));
            RandomItem();
        }
        else 
        {
            boxHp -= damage; 
            audioSource.PlayOneShot(hitSound);


        }

    }
    private void RandomItem()
    {
        int randomItem = Random.Range(0, itemPrefab.Length);
        Instantiate(itemPrefab[randomItem], transform.position, Quaternion.identity);
    }
    IEnumerator DestroyPrefabAfterDelay(GameObject prefabToDestroy)
    {
        yield return new WaitForSeconds(1);
        Destroy(prefabToDestroy);

    }
    IEnumerator DestroyParticlefterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        
        
        Destroy(gameObject);

    }



}