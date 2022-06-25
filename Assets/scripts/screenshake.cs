using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour
{
    public Transform target;
    public Vector2 seed;
    
    float speed = 20f;
    float maxMagnitude = .3f;
    float noiseMagnitude;
    Vector2 dir = Vector2.right;
    float fadeOut = 1f;
    bool shakeEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (shakeEnabled)
        {
            var sin = Mathf.Sin(speed*(seed.x+seed.y+Time.time));
            var direction = dir + GetNoise();
            direction.Normalize();
            var delta = direction*sin;

            target.localPosition = delta*maxMagnitude*fadeOut;
        }

    }

    public void FireOnce(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeAndFade(duration));
    }

    public IEnumerator ShakeAndFade(float fade_duration)
    {
        var fade_start_time = Time.time;
        var fade_end_time = fade_start_time + fade_duration;
        shakeEnabled = true;
        fadeOut = 1f;

        while (Time.time < fade_end_time)
        {
            yield return null;
            var t = 1f - Mathf.InverseLerp(fade_start_time, fade_end_time, Time.time);
            fadeOut = t;
        }
        shakeEnabled = false;
        
    }
        

    Vector2 GetNoise()
    {
        var time = Time.time;
        Vector2 noiseMagnitude = new Vector2(Mathf.PerlinNoise(seed.x, time) - .5f, Mathf.PerlinNoise(seed.y, time) - .5f);
        return noiseMagnitude;
    }
}
