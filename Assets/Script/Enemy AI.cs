using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public bool roaming = true;
    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public bool isShootable = false;
    public GameObject bulletEnemy;
    public float bulletSpeed;
    public float timeBtwFire;
    public float fireCooldown;
    public SpriteRenderer characterSR;
    public Seeker seeker;
    public Animator animator;
    bool reachDestination = false;
    Path path;
    Rigidbody2D rb;

    Coroutine moveCoroutine;

    public float freezeDurationTime;
    float freezeDuration;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        freezeDuration = 0;
        target = FindObjectOfType<Playerr>().transform;

        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;

            EnemyFireBullet();
        }
    }

    void EnemyFireBullet()
    {
        var bulletEnemyTmp = Instantiate(bulletEnemy, transform.position, Quaternion.identity);

        Rigidbody2D rbEnemy = bulletEnemyTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Playerr>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rbEnemy.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if (seeker.IsDone())
            seeker.StartPath(rb.position, target, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            path = p;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    public void FreezeEnemy()
    {
        freezeDuration = freezeDurationTime;
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {
            while (freezeDuration > 0)
            {
                freezeDuration -= Time.deltaTime;
                yield return null;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            if (force.x != 0)
                if (force.x < 0)
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                else
                    characterSR.transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }
    }

     Vector2 FindTarget()
     {
         Vector3 playerPos = FindObjectOfType<Playerr>().transform.position;
         if (roaming == true)
         {
             return (Vector2)playerPos + (Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
         }
         else
         {
             return playerPos;
        }
    }

}
