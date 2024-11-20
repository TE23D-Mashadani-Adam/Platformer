using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealthController : MonoBehaviour
{

    [SerializeField]
    float health;
    [SerializeField]
    Sprite fullHeart, halfHeart, emptyHeart;
    [SerializeField]
    Image[] hearts;

    void Update()
    {
        health = PlayerController.health;

        for (int i = 0; i < hearts.Length; i++)
        { // i = 2
            if (i < health - .5)
            {
                hearts[i].sprite = fullHeart;
            }else if(i + 1 - health == .5)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
