using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 10;
    [SerializeField] float speed = 250f;
    [SerializeField] float stun = 0.2f;
    [SerializeField] bool canHitPlayer = false;

    bool active = true;
    float lifeSpan = 1.3f;


    private void Update() {
        if (active) { 
            lifeSpan -= Time.deltaTime;
            if (lifeSpan < 0) {
                active = false;
                GetComponent<Animator>().SetBool("Splat", true);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!active) {
            return;
        }
        string tag = collision.gameObject.tag;
        if (tag == "Player") {
            if (canHitPlayer) {
                collision.GetComponent<PlayerController>().TakeDamage(1);
            } else {
                return;
            }
        } else if (tag == "Enemy") {
            collision.GetComponent<Enemy>().TakeDamage(damage, stun);
        } else if (tag == "Boss" && !canHitPlayer) {
            collision.GetComponent<BossController>().TakeDamage(damage);
        }
        active = false;
        GetComponent<Animator>().SetBool("Splat", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Finish() {
        Destroy(gameObject);
    }

    public void Shoot() {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(speed, 0));
    }
}
