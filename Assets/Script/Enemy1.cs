using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    public int enemyHp = 10;
    public int enemyMaxHp = 10;
    public int enemyAttack = 3;
    public int exp = 5;
    private GameObject player;
    private Player myPlayer;
    private Animator animator;

    public float moveSpeed = 5.0f;
    public float someThresholdDistance = 10.0f;

    private bool isChasing = false;

    public CapsuleCollider enemyCollider;

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip Scream;

    /*    private Slider healthSlider;
        private Transform healthBarTransform;*/

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayer = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        StartCoroutine(Jump());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer + 1.1f < someThresholdDistance)
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

        if (!isChasing)
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.x = 0;
            currentRotation.z = 0;
            transform.eulerAngles = currentRotation;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHp -= damage;
        if (enemyHp <= 0)
        {

            audioSource.PlayOneShot(Scream);
            animator.SetInteger("DamageType", 2);
            animator.SetTrigger("Damage");
            StartCoroutine(Die());
        }
        else
        {
            audioSource.PlayOneShot(hit);
            animator.SetInteger("DamageType", 0);
            animator.SetTrigger("Damage");
        }
    }

    IEnumerator Jump()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < someThresholdDistance)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("IsJump", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetTrigger("Jump");
                animator.SetBool("IsJump", false);
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator Die()
    {
        
        Destroy(enemyCollider);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsDie", true);
        Destroy(gameObject);
        myPlayer.Level(exp);
    }
}
