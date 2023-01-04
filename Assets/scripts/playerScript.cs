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

    void Update()
    {
        //note that health represents HP, 1 HP is half a heart
        //and then numHearts represents heart containers. 1 numHearts is 1 heart container (two healths)

        //transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * amount, transform.position.z);
        if (health < 1)
        {
            Debug.Log("GAME OVER");
            // and some other behaviours to be added
        }

        if (health > numHearts*2)
        {
            health = numHearts;
        }

        //reset everything
        foreach (Image heart in hearts) 
        {
            heart.enabled = false;
        }

        if (health % 2 == 0)
        {
            for (int i = 0; i < (health / 2); i++) 
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }

        }
        else
        {
            for (int i = 0; i < (health / 2); i++)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            hearts[health / 2].sprite = emptyHeart;
            hearts[health / 2].enabled = true;

        }

        /**
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
        **/

        goldDisplay.text = "gold: " + gold;
    }

    int state = 0;
    public void changeSprite()
    {
        if (state == 0)
        {
            renderer.sprite = playerSprites[0];
            state = 1;
        }
        else 
        {
            renderer.sprite = playerSprites[1];  
            state = 0;
        }
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
