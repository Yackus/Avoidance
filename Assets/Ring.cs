using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public GameObject attachedThought;
    public bool noMoreRings = false;

    public GameObject bird;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedThought)
        {
            if (!attachedThought.GetComponent<Thought>().beenTouched)
            {
                attachedThought.transform.position = Camera.main.WorldToScreenPoint(transform.position);

                if (Vector2.Distance( bird.transform.position, transform.position) < 3.0f)
                {
                    transform.position += 5 * (transform.position - bird.transform.position) * Time.deltaTime;
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                }
            }
            else
            {
                noMoreRings = true;
            }
            
        }

    }
}
