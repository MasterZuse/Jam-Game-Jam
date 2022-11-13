using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{

    [SerializeField] float loopStart;
    [SerializeField] float loopEnd;
    [SerializeField] AudioSource source;

    void Update() {
        if (source.time > loopEnd) {
            source.time -= loopEnd - loopStart;
        }
    }
}
