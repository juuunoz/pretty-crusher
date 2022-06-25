using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnerBehaviour : MonoBehaviour
{
    //for keeping track of all the "killable enemies", list of enemy objects
    public List<enemyScript> enemiesOnScreen = new List<enemyScript>();
    public GameObject enemyPrefab;
    public Camera mainCam;
    
    public GameObject typer;
    private string typedWord;

    public TextAsset wordsRaw;
    private string[] wordBank;

    private Quaternion enemyRotation = new Quaternion(0, 0, 0, 1);
    private Vector3 enemySpawnPosition;

    void Start()
    {
        wordBank = wordsRaw.text.Split('\n');
        for (int i=0; i<wordBank.Length; i++)
        {
            if (wordBank[i].Length > 12)
            {
                wordBank[i] = null;
            }
        }
        enemySpawnPosition = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 1));
    }

    void Update()
    {
        typedWord = typer.GetComponent<detectTyping>().typedWord;
        updateEnemyDisplays();
    }

    private void updateEnemyDisplays()
    {
        
        foreach (enemyScript x in enemiesOnScreen)
        {
            string attackText = x.attackText; //the enemies text to be typed 
            string similarWords = "";
            int i = 0;

            while (i < typedWord.Length)
            {
                if (i > attackText.Length-1 || !(typedWord[i] == attackText[i]))
                {
                    break; //no need to check if future letters match
                }
                else 
                {
                    similarWords += attackText[i];
                }
                
                i++;
            }
            x.typed = similarWords;
            x.remaining = attackText.Substring(i);
        }
    }

    //returns true if spawns an enemy
    public bool spawnEnemy(string attackWord, List<enemyScript> activeEnemies, Vector3 spawnPosition)
    {
        //TODO make sure the an enemy cannot spawned in the same y value as the last one, with a couple pixel berth
        spawnPosition.y = Random.Range(-20, 10);

        GameObject obj = Instantiate(enemyPrefab, spawnPosition, enemyRotation) as GameObject;
        enemyScript theEnemy = obj.GetComponent<enemyScript>();
        theEnemy.setDisplay(attackWord);

        activeEnemies.Add(theEnemy);
        return true;
    }

    public void debugSpawnEnemy()
    {
        string word = null;
        while (word == null)
        {
            word = wordBank[Random.Range(0, wordBank.Length)];
        }
        spawnEnemy(word, enemiesOnScreen, enemySpawnPosition);
    }

}
