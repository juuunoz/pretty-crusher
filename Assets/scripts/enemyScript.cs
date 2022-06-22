using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyScript : MonoBehaviour
{
    public TextMeshProUGUI display;
    public string attackText;

    public GameObject typer;
    private GameObject playerResources;
    private playerScript player;

    private float movespeed;
    private Vector3 movement;
    private string textColor;
    private string typedTextColor;
    private int goldValue = 5;

    public string typed = "";
    public string remaining = "";

    void Start()
    {
        playerResources = GameObject.FindWithTag("playerResources");
        player =  playerResources.GetComponent<playerScript>();
        movespeed = 4f;
        movement = new Vector3(-movespeed, 0, 0);
    
        textColor = "red";
        typedTextColor = "green";

        
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

    public void die()
    {
        player.addGold(goldValue);
        Destroy(gameObject);
    }

    public void setDisplay(string theDisplay)
    {
        display.text = attackText = theDisplay;
    }
}
