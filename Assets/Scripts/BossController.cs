using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] int health = 150;
    [SerializeField] float shortCooldown = 0.75f;
    [SerializeField] float longCooldown = 3f;

    [SerializeField] Projectile projectile;

    bool fightActive = false;
    float currentCooldown;
    int nextCoolDown = 0;

    Animator animator;
    GameObject playerObj;

    void Start() {
        animator = GetComponent<Animator>();
        currentCooldown = longCooldown;
        playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update() {
        if (fightActive) {
            currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0);
            if (currentCooldown <= 0) {
                animator.SetBool("Throw", true);
            }
        }
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            animator.SetBool("Dead", true);
        }
    }

    private void StayDead() {
        animator.SetBool("Stay Dead", true);
    }

    private void Goodbye() {
        Destroy(gameObject);
    }

    public void Activate() {
        fightActive = true;
    }

    private void Attack() {
        animator.SetBool("Throw", false);
        int rand = (int) Mathf.Floor(Random.Range(0, 3));
        if (rand == 0) {
            BasicAttack();
        } else if (rand == 1) {
            CircleAttack();
        } else {
            CircleAttack2();
        }
        nextCoolDown = (nextCoolDown + 1) % 3;
        if (nextCoolDown == 0) {
            currentCooldown = longCooldown;
        } else {
            currentCooldown = shortCooldown;
        }
    }

    private void LaunchProjectile(float angle) {
        Projectile p = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        p.Shoot();
    }

    private void BasicAttack() {
        var dir = playerObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        LaunchProjectile(angle);
    }

    private void CircleAttack() {
        for (int i = 0; i < 8; i++) {
            LaunchProjectile(i * 45);
        }
    }

    private void CircleAttack2() {
        for (int i = 0; i < 8; i++) {
            LaunchProjectile(i * 45 + 22.5f);
        }
    }
}
