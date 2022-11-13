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

    void Start() {
        distanceLeft = patrolDistance;
    }

    void Update() {
        if (spottedPlayer) {
            ChasePlayer();
        } else {
            Patrol();
            CheckPlayerDistance();
        }
    }

    private void ChasePlayer() {

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

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
