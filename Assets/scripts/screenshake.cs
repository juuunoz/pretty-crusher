using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour
{
    public static ScreenshakeController instance;

    public float shakeDuration;
    public float shakePower;
    private float shakeFadeDuration;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            shakeScreen(0.15f, .5f);
        }
    }

    private float shakeX = 0f;
    private float shakeY = 0f;

    private void LateUpdate() 
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            transform.position += new Vector3(-shakeX, -shakeY, 0f);
            

            transform.position += new Vector3(shakeX, shakeY, 0f);
            shakePower = Mathf.MoveTowards(shakePwer, 0f, shakeFadeDuration*Time.deltaTime);
        }
        
    }

    private void shakeScreen(float duration, float power)
    {
        shakeDuration = duration;
        shakePower = power;
        shakeX = Random.Range(-1f,1f) * shakePower;
        shakeY = Random.Range(-1f,1f) * shakePower;

        shakeFadeDuration = power/duration;
    }
}
