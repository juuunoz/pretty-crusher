using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

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
    private List<string> activeWordBank;
    int activeWordBankSize;

    private Quaternion enemyRotation = new Quaternion(0, 0, 0, 1);
    private Vector3 enemySpawnPosition;

    bool currentlySpawning = false;
    float minFreq;
    float maxFreq;
    int minstrlen;
    int maxstrlen;

    void Start()
    {
        
        activeWordBank = new List<string>();
        activeWordBankSize = 0;
        
        //defaultvalues
        minFreq = 1.5f;
        maxFreq = 2.5f;
        minstrlen = 4;
        maxstrlen = 6;

        wordBank = wordsRaw.text.Split('\n');
        //mergeSort(wordBank, 0, wordBank.Length-1);
        for (int i = 0; i < wordBank.Length; i++)
        {
            if (wordBank[i].Length > 12)
            {
                wordBank[i] = null;
            }
            else
            {
                if (wordBank[i].Length > 1)
                {
                    wordBank[i] = wordBank[i].Remove(wordBank[i].Length - 1, 1);
                }
            }
        }
        //Array.Sort(wordBank);

        for (int i = 0; i < wordBank.Length; i++) 
        {
            if (wordBank[i] != null && wordBank[i].Length >= minstrlen && wordBank[i].Length <= maxstrlen) 
            {
                activeWordBank.Add(wordBank[i]);
                activeWordBankSize++;
            }
        }

        enemySpawnPosition = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 1));
        enemySpawnPosition.z = 0;
    }


    void Update()
    {
        typedWord = typer.GetComponent<detectTyping>().typedWord;
        updateEnemyDisplays();
    }

    public int getMinstrlen() { return minstrlen; }
    public int getMaxstrlen() { return maxstrlen; }

    public void changeSpawnParameters(float minFreq, float maxFreq)  
    {
        //i suppose it's not safe to clear the list and rebuild it
        //because the spawnenemies function runs on its own thread


        //update the list activeWordBank
        //add everything less than maxstrlen from wordBank
        //remove everything less than minstrlen from activeWordBank
        this.minFreq = minFreq;
        this.maxFreq = maxFreq;
        
        //startSpawningEnemies();

    }

    public void increaseMaxstrlen() 
    {
        //increases the maximum length of strings in the active word bank by 1
        for (int i = 0; i < wordBank.Length; i++) 
        {
            if (wordBank[i] != null && wordBank[i].Length == maxstrlen + 1) 
            {
                activeWordBank.Add(wordBank[i]);
                activeWordBankSize++;
            }
        }
        maxstrlen++;
        UnityEngine.Debug.Log("New max string length: " + maxstrlen);
    }

    public void increaseMinstrlen() 
    {
        for (int i = 0; i < activeWordBank.Count; i++) 
        {
            if (activeWordBank[i] != null && activeWordBank[i].Length == minstrlen) 
            {
                activeWordBank.Remove(activeWordBank[i]);
                activeWordBankSize--;
                i--;
            }
        }
        minstrlen++;
        UnityEngine.Debug.Log("New min string length: " + minstrlen);
    }

    public void startSpawningEnemies() 
    {
        if (currentlySpawning)
        {
            StopCoroutine("spawnEnemies");
            currentlySpawning = false;
        }
        else 
        {
            StartCoroutine(spawnEnemies(minFreq, maxFreq, minstrlen, maxstrlen));
            currentlySpawning = true;
        }
    }

    private IEnumerator spawnEnemies(float minFrequency, float maxFrequency, int minstrlen, int maxstrlen) //strlen stuff not implemented yet
    {
        //remove minstrlen and maxstrlen, get a value from a given list<String> instead, keep this string updated with every call to the change function
        while (true) 
        {
            string word = null;
            do
            {
                int rand = UnityEngine.Random.Range(0, activeWordBankSize);
                UnityEngine.Debug.Log(rand);
                word = activeWordBank[rand];
            } while ((word == null) || (word.Length < minstrlen || word.Length > maxstrlen));
            
            spawnEnemy(word, enemiesOnScreen, enemySpawnPosition);
            yield return new WaitForSeconds(UnityEngine.Random.Range(minFrequency, maxFrequency));
        }
    }

    //private void merge(string[] x, int l, int m,, int r){};

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

    float lastypos = -30;
    //returns true if spawns an enemy
    public bool spawnEnemy(string attackWord, List<enemyScript> activeEnemies, Vector3 spawnPosition)
    {
        Debug.Log("Spawning enemy with word: " + attackWord);
        spawnPosition.y = UnityEngine.Random.Range(-10, 10);
        while (spawnPosition.y == lastypos) spawnPosition.y = UnityEngine.Random.Range(-20, 10);
        spawnPosition.x = transform.position.x;

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
            word = activeWordBank[UnityEngine.Random.Range(0, activeWordBankSize)];
        }
        Debug.Log("Spawning enemy with attackword: " + word);
        spawnEnemy(word, enemiesOnScreen, enemySpawnPosition);
    }

    public void testchangeSpawnParameters() 
    {
        
    }

}
