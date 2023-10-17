using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{

    private Player player;
    public int maxHealth;
    public int enemyHp;
    public int exp;
    public Transform target;
    public bool isChase;
    public bool isAttack = false;
    public BoxCollider bossCollider;
    public AudioSource audioSource;
    public AudioClip shotAudio;
    public AudioClip deadAudio;

    public int bossAttack = 5;

    Rigidbody rigid;
    Animator anim;

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;

    private bool isChasing = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();

        if(player == null)
        {
            Debug.Log("플레이어를 못 찾음");
        }
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    private void Update()
    {
        if(isAttack == false && target != null)
        {
        StartCoroutine(MissileShot());
        }
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        Vector3 targetPosition = player.transform.position;
        transform.LookAt(targetPosition);
    }
    public void TakeDamage(int damage)
    {
        enemyHp -= damage;
        if (enemyHp <= 0)
        {

            anim.SetTrigger("doDie");
            audioSource.PlayOneShot(deadAudio);
            StartCoroutine(Die());
        }
        else
        {
            //audioSource.PlayOneShot(hit);
            anim.SetTrigger("doDamage");
        }
    }
    IEnumerator Die()
    {

        Destroy(bossCollider);
        yield return new WaitForSeconds(4.0f);
        //anim.SetBool("IsDie", true);
        Destroy(gameObject);
        player.Level(exp);
    }

    IEnumerator MissileShot()
    {
        isAttack = true;
        anim.SetTrigger("doAttack");
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        EnemyBullet bossMissileA = instantMissileA.GetComponent<EnemyBullet>();
        bossMissileA.target = target;
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        EnemyBullet bossMissileB = instantMissileB.GetComponent<EnemyBullet>();
        bossMissileB.target = target;
        yield return new WaitForSecondsRealtime(3.0f);
        isAttack = false;
        Destroy(instantMissileA, 3);
        Destroy(instantMissileB, 3);
    }


}
