using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    AudioSource audioSource;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        if (!audioSource)
        {
            GameObject am = GameObject.Find("AudioManager");

            if (am)
            {
                audioSource = am.GetComponent<AudioSource>();

                slider.value = audioSource.volume;
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
