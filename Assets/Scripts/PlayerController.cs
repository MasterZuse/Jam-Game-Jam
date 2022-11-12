using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 10.0f;
    [SerializeField] Projectile currentProjectile;

    bool canMove = true;
    bool bagOpen = false;

    Animator animator;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SpaceButton();
    }


    private void Move() {
        if (!canMove) {
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(horizontal, vertical, 0);

        if (horizontal < 0) { 
            sprite.flipX = true;
        } else if (horizontal > 0) {
            sprite.flipX = false;
        }

        float greatestSpeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        if (greatestSpeed > 0) {
            bagOpen = false;
        }

        animator.SetFloat("Speed", greatestSpeed);
    }

    private void SpaceButton() {
        if (Input.GetAxisRaw("Jump") > 0) {
            if (!bagOpen) {
                canMove = false;
                animator.SetBool("Unsling", true);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void Fire() {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Projectile p = Instantiate(currentProjectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        p.Shoot();
    }

    private void FinishUnsling() {
        bagOpen = true;
        canMove = true;
        animator.SetBool("Unsling", false);
    }

}
