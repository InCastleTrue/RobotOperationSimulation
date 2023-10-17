using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    public float walkSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float dashForce = 2.0f;
    private Vector3 dir = Vector3.zero;
    private bool isGround = true;
    public bool isDashing = false;
    public bool isShooting = false;
    public Animator animator;
    public GameObject ShootingPrefab;
    public Transform ShotPos;
    private float LuncherForce = 30.0f;
    public int damage = 2;
    public int playerHp = 20;
    public int playerMaxHp = 20;
    private Enemy1 enemy1;
    private ItemBox itemBox;

    public BoxCollider playerCollider;


    public int level = 1;
    public int exp = 0;
    public int maxExp = 15;
    public int status = 0;
    public int skillPoint = 0;

    public float bulletReload = 0.6f;
    public float dashReload = 1.7f;

    public GameObject aimingReticle;

    private GameManager gameManager;

    private Camera mainCamera; // Reference to the main camera
    
    public bool isDoubleAttackUp = false;
    public bool isDashUP = false;
    public bool isBeamUp = false;
    public bool isBeam = false;
    public bool isPlayerDying = false;
    
    public ParticleSystem attackParticle;
    public ParticleSystem levelUpParticle;
    public ParticleSystem dyingParticle;

    public GameObject beamSpawnPoint;
    public GameObject levelUpSpawnPoint;


    public CameraMove cameraMove;
    public AudioClip walkSound;
    public AudioClip attackSound;
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip beamSound;
    public AudioClip levelSound;
    public AudioClip diyingSound;

    public bool isWalkSound = false;

    public AudioSource audioSource;

    public bool isClear = false;

    

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main; // Assign the main camera
        gameManager = FindObjectOfType<GameManager>();
        itemBox = FindObjectOfType<ItemBox>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        cameraMove = FindObjectOfType<CameraMove>();
        aimingReticle.SetActive(false);
    }

    public void Update()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            enemy1 = enemyObject.GetComponent<Enemy1>();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && isGround && isBeamUp && !isBeam)

        {
           StartCoroutine(Beam());
        }

        // Get the camera's forward direction without vertical component
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        dir = Input.GetAxis("Horizontal") * mainCamera.transform.right +
              Input.GetAxis("Vertical") * camForward;

        if (dir.magnitude > 0 && isGround ==true)
        {
            animator.SetBool("IsWalk", true);
            transform.forward = camForward;
            if(isWalkSound == false && Time.timeScale == 1) 
            {
                isWalkSound = true;
            StartCoroutine(WalkSound());
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }

        dir.Normalize();

        if (Input.GetButton("Jump") && isGround == true && Time.timeScale == 1)
        {
            animator.SetBool("IsJump", true);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.PlayOneShot(jumpSound);
            isGround = false;
        }

        if (Input.GetButton("Dash") && isDashing == false && isGround == true && !isDashUP && Time.timeScale == 1)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetButton("Dash") && isDashing == false && isGround == true && isDashUP && Time.timeScale == 1)
        {
            StartCoroutine(SuperDash());
        }
        if (Input.GetMouseButton(0) && isGround == true && isShooting == false && !gameManager.IsOptionActive() && !isDoubleAttackUp && Time.timeScale == 1)
        {
            StartCoroutine(Shooting());

        }
        else if (Input.GetMouseButton(0) && isGround == true && isShooting == false && !gameManager.IsOptionActive() && isDoubleAttackUp && Time.timeScale == 1)
        {
            StartCoroutine(DoubleShooting());
        }

            if (Input.GetMouseButton(1) && !gameManager.IsOptionActive())
        {
            aimingReticle.SetActive(true);
        }
        else
        {
            aimingReticle.SetActive(false);
        }
        
            
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(this.gameObject.transform.position + dir * walkSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("ItemBox"))
        {
            isGround = true;
            animator.SetBool("IsJump", false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy1 enemy = collision.gameObject.GetComponent<Enemy1>();
            if (enemy != null)
            {
                HitDamage(enemy.enemyAttack);
            }
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                HitDamage(boss.bossAttack);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Clear"))
        {
            isClear = true;
        }
    }

    IEnumerator DyingActive()
    {
        yield return new WaitForSecondsRealtime(3);
        isPlayerDying = true;
        //gameObject.SetActive(false);
        //Destroy(playerCollider);
        

    }

    public void Level(int gainExp)
    {
        exp += gainExp;
        if(exp >= maxExp)
        {
            audioSource.PlayOneShot(levelSound);
            GameObject levelObject = Instantiate(levelUpParticle.gameObject, levelUpSpawnPoint.transform.position, Quaternion.identity);
            ParticleSystem levelParticle = levelObject.GetComponent<ParticleSystem>();
            levelParticle.Play();
            level++;
            exp = exp - maxExp;
            maxExp *= 2;
            status += 3;
            if (level % 5 == 0)
            {
                skillPoint++;
            }
        }
    }

    

    IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashVelocity = transform.forward * dashForce;
        playerRb.velocity = dashVelocity;
        animator.SetBool("IsRunning", true);
        audioSource.PlayOneShot(dashSound);
        yield return new WaitForSecondsRealtime(0.3f);
        animator.SetBool("IsRunning", false);
        yield return new WaitForSecondsRealtime(dashReload);
        isDashing = false;
        playerRb.velocity = Vector3.zero;
    }
    IEnumerator SuperDash()
    {
        isDashing = true;
        Vector3 dashVelocity = transform.forward * dashForce * 1.5f;
        playerRb.velocity = dashVelocity;
        animator.SetBool("IsRunning", true);
        audioSource.PlayOneShot(dashSound);
        yield return new WaitForSecondsRealtime(0.3f);
        animator.SetBool("IsRunning", false);
        yield return new WaitForSecondsRealtime(dashReload);
        isDashing = false;
        playerRb.velocity = Vector3.zero;
    }

    IEnumerator Shooting()
    {
        Vector3 luncher = transform.forward;
        isShooting = true;
        animator.SetBool("IsShooting", true);
        yield return new WaitForSecondsRealtime(0.4f);
        audioSource.PlayOneShot(attackSound);
        GameObject bullet = Instantiate(ShootingPrefab);
        bullet.transform.position = ShotPos.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(luncher * LuncherForce, ForceMode.Impulse);
        animator.SetBool("IsShooting", false);
        yield return new WaitForSecondsRealtime(bulletReload);
        isShooting = false;
    }
    IEnumerator DoubleShooting()
    {
        Vector3 luncher = transform.forward;
        isShooting = true;
        animator.SetBool("IsShooting", true);

        yield return new WaitForSecondsRealtime(0.4f);

        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(ShootingPrefab);
            bullet.transform.position = ShotPos.transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(luncher * LuncherForce, ForceMode.Impulse);
            audioSource.PlayOneShot(attackSound);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        animator.SetBool("IsShooting", false);
        yield return new WaitForSecondsRealtime(bulletReload*1.5f);
        isShooting = false;
    }


    IEnumerator Beam()
    {

        animator.SetTrigger("IsBeam");
        isBeam = true;
        audioSource.PlayOneShot(beamSound);
        yield return new WaitForSecondsRealtime(1.0f);
        GameObject beamObject = Instantiate(attackParticle.gameObject, beamSpawnPoint.transform.position, Quaternion.identity);
        ParticleSystem beamParticle = beamObject.GetComponent<ParticleSystem>();
        Transform beamTransform = beamObject.transform;
        beamTransform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        walkSpeed = 0;
        isShooting = true;
        isDashing = true;
        isGround = false;
        beamParticle.Play();

        
        cameraMove.sensitivityX = 0;
        cameraMove.sensitivityY = 0;
        yield return new WaitForSecondsRealtime(1.0f);
        walkSpeed = 5.0f;
        beamParticle.Stop();
        isGround = true;

        cameraMove.sensitivityX = 2;
        cameraMove.sensitivityY = 2;
        yield return new WaitForSecondsRealtime(1.0f);
        isShooting = false;
        isDashing = false;
        yield return new WaitForSecondsRealtime(7.0f);
        isBeam = false;
        
        Destroy(beamParticle);


    }

    public void HitDamage(int damage)
    {
        playerHp -= damage;
        animator.SetTrigger("isHit");
        audioSource.PlayOneShot(hitSound);
        if (playerHp <= 0)
        {
            //쓰러지고 파티클 나오고 콜라이더 없애고 비활성화
            animator.SetTrigger("isDying");
            Destroy(playerCollider);
            playerRb.useGravity = false;
            playerRb.mass = 500;
            audioSource.PlayOneShot(diyingSound);
            GameObject dyingObject = Instantiate(dyingParticle.gameObject, levelUpSpawnPoint.transform.position, Quaternion.identity);
            ParticleSystem dyingPlayerParticle = dyingObject.GetComponent<ParticleSystem>();

            dyingPlayerParticle.Play();


            StartCoroutine(DyingActive());

        }
    }
    IEnumerator WalkSound()
    {
        audioSource.PlayOneShot(walkSound);
        yield return new WaitForSecondsRealtime(0.6f);
        
        isWalkSound = false;
    }


}


/*void CheckGround()
{
    RaycastHit hit;

    if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.3f, layer ))// 이동하다보면 발끝이 땅에 묻히면 검출이 불가해서 위쪽 방향에서 살짝 위로 둠 쏘는 곳, 쏘는 방향, 얼마나 거리를 쏘나
        //레이저를 쏠건데 플레이어 발끝보다 0.2만큼 높은 위치에서 아래 방향으로 0.4만큼 까지만 레이저가 발사된다. 이 거리내에서 layer가 검출되면 그 정보를 hit에 담아라
    {
        Ground = true;
    }
    else
    {
        Ground = false;
    }
}*/
