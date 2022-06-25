using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class detectTyping : MonoBehaviour
{
    public TextMeshProUGUI display;
    public GameObject enemySpawner;
    public string typedWord = "";
    private int maxTypedWords = 12;

    public GameObject player;
    private playerScript pscript;

    private List<enemyScript> enemiesOnScreen;

    void Start()
    {
        pscript = player.GetComponent<playerScript>();
    }

    void Update()
    {
    
        foreach (char c in Input.inputString)
        {
            if ((c == '\b')) // delete the char
            {
                if (typedWord.Length > 0)
                {
                    typedWord = typedWord.Substring(0, typedWord.Length - 1);
                }
            }
            else if ((c == ' ') || (c == '\n') || (c == '\r')) //clear the char
            {
                if (destroyEnemy(typedWord))
                {
                    pscript.changeSprite();
                }
                typedWord = "";
            }
            else if (typedWord.Length <= maxTypedWords) //type the char
            {
                typedWord += c;
            }
        }
        display.text = typedWord;
    }

    private bool destroyEnemy(string word)
    {
        //update how many enemies are on the screen
        enemiesOnScreen = enemySpawner.GetComponent<spawnerBehaviour>().enemiesOnScreen;

        foreach (enemyScript x in enemiesOnScreen)
        {
            if (x.attackText == word)
            {
                x.die(); //dont worry.. this only kills one enemy with that word
                enemiesOnScreen.Remove(x);
                this.GetComponent<screenshake>().FireOnce(.5f);
                return true;
            }
        }
        return false;
    }
}
