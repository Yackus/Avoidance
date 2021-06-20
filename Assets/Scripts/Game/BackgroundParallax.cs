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
    }

    void FixedUpdate()
    {
        Scroll();
    }

    void CheckPosition()
    {
        if (background1.transform.position.x < -24) background1.transform.position = new Vector3(background3.transform.position.x + 24, 0, 0);
        if (background2.transform.position.x < -24) background2.transform.position = new Vector3(background1.transform.position.x + 24, 0, 0);
        if (background3.transform.position.x < -24) background3.transform.position = new Vector3(background2.transform.position.x + 24, 0, 0);
    }

    void Scroll()
    {
        background1.transform.position -= new Vector3(Time.deltaTime * scrollSpeed, 0,0);
        background2.transform.position -= new Vector3(Time.deltaTime * scrollSpeed, 0,0);
        background3.transform.position -= new Vector3(Time.deltaTime * scrollSpeed, 0,0);
    }
}
