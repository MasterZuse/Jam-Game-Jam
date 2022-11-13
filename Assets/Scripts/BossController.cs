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

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        currentCooldown = longCooldown * 2;
    }

    // Update is called once per frame
    void Update() {
        if (fightActive) {
            currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0);
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
}
