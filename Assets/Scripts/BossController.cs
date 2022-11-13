using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] int health = 150;
    [SerializeField] float attackSpeed = 2.5f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
