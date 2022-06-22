using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerScript : MonoBehaviour
{
    public int health;
    public int numHearts;

    public TextMeshProUGUI goldDisplay;
    public int gold = 0;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public SpriteRenderer renderer;
    public Sprite[] playerSprites;

    float speed = 1.5f; //how fast it shakes
    float amount = 3.0f; //how much it shakes
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * amount, transform.position.z);
        if (health < 1)
        {
            Debug.Log("GAME OVER");
            // and some other behaviours to be added
        }

        if (health > numHearts)
        {
            health = numHearts;
        }

        for (int i=0; i<hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else 
            {
                hearts[i].enabled = false;
            }
        }

        goldDisplay.text = "gold: " + gold;
    }

    public void changeSprite()
    {
        renderer.sprite = playerSprites[Random.Range(0, 2)];
    }

    public void addGold(int x)
    {
        gold += x;
    }

    public void takeDamage()
    {
        health -= 1;
    }
}
