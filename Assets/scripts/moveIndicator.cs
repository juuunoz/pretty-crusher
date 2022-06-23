using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveIndicator : MonoBehaviour
{
    public GameObject progressBar;
    private float barWidth;
    
    public int max;
    public float current;
    public float increment;
    private float barPosition;

    // Start is called before the first frame update
    void Start()
    {
        max = 100;
        current = 0.0f;
        increment = 3.0f;
        barWidth = progressBar.GetComponent<RectTransform>().rect.width;
        this.transform.localPosition = new Vector3(-(barWidth/2), 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        current += increment*Time.deltaTime;
        if (current > max)
        {
            Debug.Log("Reached end of level!");
            current = max;
        }
        else 
        {
            barPosition = barWidth*(current/max) - barWidth/2;
            this.transform.localPosition = new Vector3(barPosition, 0, transform.position.z); 
        }
        
    }
}
