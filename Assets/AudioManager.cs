using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource manager;
    bool isPlaying = false;
    float timer = 0;
    float timerTotal = 1f;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!isPlaying)
        {
            if (timer >= timerTotal)
            {
                isPlaying = true;
                manager.Play();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Mathf.Approximately(manager.volume,0.35f) && isPlaying)
        {
            manager.volume = Mathf.Lerp(manager.volume, 0.35f, 0.005f);
        }
    }
}
