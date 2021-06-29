using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : MonoBehaviour
{
    public GameObject text;
    public Camera cam;

    public bool onMouse = false;
    public bool beenTouched = false;

    public bool canMove;

    private void Awake()
    {
        cam = GameObject.Find("PixelPerfectCamera").GetComponent<Camera>(); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        if (canMove && Input.GetMouseButton(0))
        { 
            Debug.Log("down");
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (Vector2.Distance((Vector2)(Input.mousePosition), (Vector2)(transform.position)) < 150)
            {
                Debug.Log("on obj");
                beenTouched = true;
                onMouse = true;
            }
        }
        else
        {
            onMouse = false;
        }

        if (onMouse)
        {
            Vector2 pos = Input.mousePosition;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.velocity = Vector2.ClampMagnitude((pos - (Vector2)transform.position) * 20, 500);
        }

        if (transform.position.x < -100 ||
            transform.position.x > Screen.width + 100 ||
            transform.position.y < -100 ||
            transform.position.y > Screen.height + 100)
        {
            Destroy(gameObject);
        }
    }
}
