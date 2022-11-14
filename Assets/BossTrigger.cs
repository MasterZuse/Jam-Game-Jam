using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.GetComponentInChildren<SongPlayer>().ChangeSong();
            GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<BossController>().Activate();
            Destroy(gameObject);
        }
    }
}
