using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public GameObject background3;

    public float scrollSpeed;

    void Update()
    {
        CheckPosition();
        Scroll();
    }

    /// <summary>
    /// Update positions so that they loop back around
    /// </summary>
    void CheckPosition()
    {
        if (background1.transform.position.x < -24) background1.transform.position = new Vector3(background3.transform.position.x + 24, 0, 0);
        if (background2.transform.position.x < -24) background2.transform.position = new Vector3(background1.transform.position.x + 24, 0, 0);
        if (background3.transform.position.x < -24) background3.transform.position = new Vector3(background2.transform.position.x + 24, 0, 0);
    }

    //Move images based on delta time
    void Scroll()
    {
        Vector3 movement = new Vector3(Time.deltaTime * scrollSpeed, 0, 0);

        background1.transform.position -= movement;
        background2.transform.position -= movement;
        background3.transform.position -= movement;
    }
}
