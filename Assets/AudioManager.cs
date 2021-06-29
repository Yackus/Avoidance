using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ringDing;
    public AudioClip click;

    AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRingDing()
    {
        m_audioSource.PlayOneShot(ringDing);
    }

    public void PlayClick()
    {
        m_audioSource.PlayOneShot(click);
    }
}
