using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 10;
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 250f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag == "Player") {
            return;
        } else if (tag == "Enemy") {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void Shoot() {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(speed, 0));
    }
}
