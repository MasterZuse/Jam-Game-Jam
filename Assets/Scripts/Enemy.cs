using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class Enemy : MonoBehaviour
{

    [SerializeField] int health = 50;
    [SerializeField] float speed = 2f;
    [SerializeField] float visionDistance = 3f;
    [SerializeField] Direction patrolDirection;
    [SerializeField] float patrolDistance;
    float distanceLeft;
    bool spottedPlayer = false;
    float stun = 0f;

    void Start() {
        distanceLeft = patrolDistance;
    }

    void Update() {
        if (stun > 0) {
            stun = Mathf.Max(stun - Time.deltaTime, 0);
            return;
        }
        if (spottedPlayer) {
            ChasePlayer();
        } else {
            Patrol();
            CheckPlayerDistance();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (stun > 0) {
            return;
        }
        string tag = collision.gameObject.tag;
        if (tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }

    private void ChasePlayer() {
        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, speed * Time.deltaTime);
    }

    private void CheckPlayerDistance() {
        GameObject playerObj = GameObject.FindGameObjectsWithTag("Player")[0];
        if (Vector3.Distance(transform.position, playerObj.transform.position) <= visionDistance) {
            spottedPlayer = true;
        }
    }

    private void Patrol() {
        float movement = speed * Time.deltaTime;
        if (patrolDirection == Direction.LEFT) {
            transform.Translate(-movement, 0, 0);
            distanceLeft -= movement;
            if (distanceLeft <= 0) {
                distanceLeft = patrolDistance;
                patrolDirection = Direction.RIGHT;
            }
        } else if (patrolDirection == Direction.RIGHT) {
            transform.Translate(movement, 0, 0);
            distanceLeft -= movement;
            if (distanceLeft <= 0) {
                distanceLeft = patrolDistance;
                patrolDirection = Direction.LEFT;
            }
        } else if (patrolDirection == Direction.UP) {
            transform.Translate(0, movement, 0);
            distanceLeft -= movement;
            if (distanceLeft <= 0) {
                distanceLeft = patrolDistance;
                patrolDirection = Direction.DOWN;
            }
        } else if (patrolDirection == Direction.DOWN) {
            transform.Translate(0, -movement, 0);
            distanceLeft -= movement;
            if (distanceLeft <= 0) {
                distanceLeft = patrolDistance;
                patrolDirection = Direction.UP;
            }
        }
    }

    public void TakeDamage(int amount, float stunAmount) {
        health -= amount;
        stun += stunAmount;
        spottedPlayer = true;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
