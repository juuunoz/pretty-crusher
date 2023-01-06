using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System;

public class enemyScript : MonoBehaviour
{
    public TMP_FontAsset FontAsset;
    public TextMeshProUGUI display;
    private TMP_Text displayText;
    public string attackText;

    public GameObject typer;
    private GameObject playerResources;
    private playerScript player;

    public float movespeed;
    private float ypos;
    private Vector3 movement;
    public string textColor;
    public string typedTextColor;
    private int goldValue = 5;

    public string typed = "";
    public string remaining = "";


    void Start()
    {
        playerResources = GameObject.FindWithTag("playerResources");
        player =  playerResources.GetComponent<playerScript>();
        //movespeed = 8f;
        movement = new Vector3(-movespeed, 0, 0);

        ypos = this.transform.position.y;

        displayText = display.GetComponent<TMP_Text>();
        displayText.font = FontAsset;

        StartCoroutine(oscillate()) ;
        
    }

    void Update()
    {
        
        if (transform.position.x < playerResources.transform.position.x)
        {
            player.takeDamage();
            Destroy(gameObject);
        }

        transform.position += movement * Time.deltaTime;
        display.text = "<color=" + typedTextColor + ">" + typed + "</color>" + "<color=" + textColor + ">" + remaining+ "</color>";
    
    }

    IEnumerator oscillate()
    {
        float inc = 0.0f;
        //UnityEngine.Debug.Log("coroutine");
        while (true) 
        {
            inc += 0.01f;
            transform.position = new Vector3(transform.position.x, (float)(ypos + Math.Sin(inc)), transform.position.z);
            yield return null;
        }
        
        
    }

    public void die()
    {
        //UnityEngine.Debug.Log("was attacked!");
        player.addGold(goldValue);
        Destroy(gameObject);
    }

    public void setDisplay(string theDisplay)
    {
        display.text = attackText = theDisplay;
    }
}
