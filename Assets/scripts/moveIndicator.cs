using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveIndicator : MonoBehaviour
{
    public GameObject progressBar;
    private float barWidth;

    // Start is called before the first frame update
    void Start()
    {
        barWidth = progressBar.GetComponent<RectTransform>().rect.width;
        transform.position = new Vector3(progressBar.transform.position.x - barWidth/2.0f, progressBar.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
