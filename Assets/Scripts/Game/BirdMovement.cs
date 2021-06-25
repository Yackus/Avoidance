using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    Vector2 velocity;
    Animator anim;

    [Header("Controls")]
    public float gravity = 5.0f;
    public float flapForce = 5.0f;

    [Header("Restrictions")]
    public float maxHeight;
    public float minHeight;

    bool started = false;
    bool belowMaxHeight = true;
    bool aboveMinHeight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckInput();
        CheckBoundaries();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void CheckInput()
    {
        if (belowMaxHeight)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("space"))
            {
                velocity.y = flapForce;
                if (!started) started = true;
            }
        }
    }
    
    void CheckBoundaries()
    {
        if(transform.position.y >= maxHeight)
        {
            belowMaxHeight = false;
        }
        else
        {
            belowMaxHeight = true;
        }

        if(transform.position.y <= minHeight)
        {
            aboveMinHeight = false;
        }
        else
        {
            aboveMinHeight = true;
        }
    }

    void UpdateAnimations()
    {
        anim.SetFloat("yVel", velocity.y);
    }

    void ApplyMovement()
    {
        if (started)
        {
            velocity.y -= gravity * Time.deltaTime;

            if(!aboveMinHeight)
            {
                velocity.y = flapForce;
            }

            transform.position += new Vector3(0, velocity.y, 0) * Time.deltaTime;
        }
    }

    public Vector2 GetVel()
    {
        return velocity;
    }
}
