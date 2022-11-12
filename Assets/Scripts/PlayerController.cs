using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 10.0f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(horizontal, vertical, 0);

        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));
    }

    private void Fire()
    {
        //Shoot the jam
    }
}
