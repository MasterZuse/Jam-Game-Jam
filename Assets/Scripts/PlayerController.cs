using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {

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
    }
}
