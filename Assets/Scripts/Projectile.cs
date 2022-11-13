using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 10;
    [SerializeField] float speed = 250f;

    bool active = true;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (!active) {
            return;
        }
        string tag = collision.gameObject.tag;
        if (tag == "Player") {
            return;
        } else if (tag == "Enemy") {
            collision.GetComponent<Enemy>().TakeDamage(damage);
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
