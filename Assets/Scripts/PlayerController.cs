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

        if (bagOpen) {
            sprite.flipX = Mathf.Abs(GetMouseAngle()) > 90;
        }
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
        animator.SetFloat("Speed", greatestSpeed);
    }

    private void SpaceButton() {
        if (Input.GetAxisRaw("Jump") > 0) {
            if (!bagOpen) {
                canMove = false;
                animator.SetBool("Unsling", true);
                animator.SetFloat("Speed", 0);
            } else {
                animator.SetBool("Sling", true);
            }
        }
    }

    private void Fire() {
        var angle = GetMouseAngle();
        Projectile p = Instantiate(currentProjectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        p.Shoot();
    }

    private float GetMouseAngle() {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void FinishUnsling() {
        bagOpen = true;
        animator.SetBool("Unsling", false);
    }

    private void FinishSling() {
        animator.SetBool("Sling", false);
        canMove = true;
        bagOpen = false;
    }

}
