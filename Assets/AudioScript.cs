using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!audioSource)
        {
            GameObject am = GameObject.Find("AudioManager");

            if (am)
            {
                audioSource = am.GetComponent<AudioSource>();
            }
        }
    }

    public void SetVolume(float _vol)
    {
        if (audioSource)
        {
            audioSource.volume = _vol;
        }
    }
}
