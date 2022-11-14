using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] Sprite[] sprites;


    public void UpdateHealthBar(int health) {
        image.sprite = sprites[health];
    }
}
