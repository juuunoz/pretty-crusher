using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
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
        barWidth = progressBar.GetComponent<RectTransform>().rect.width;
        this.transform.localPosition = new Vector3(-(barWidth / 2), 3, -1);
        UnityEngine.Debug.Log(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void startLevel() 
    {
        StartCoroutine(startCountdown(30));
    }

    IEnumerator startCountdown(int max) 
    {
        current = 0;
        while (current < max) {
            current += increment * Time.deltaTime;
            if (current > max)
            {
                UnityEngine.Debug.Log("Reached end of level!");
                current = max;
                yield break;
            }
            else
            {
                barPosition = barWidth * (current / max) - barWidth / 2;
                this.transform.localPosition = new Vector3(barPosition, 3, -1);
            }
            yield return null;
        }

    }
}
