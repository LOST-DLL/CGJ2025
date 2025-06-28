using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllerBase : MonoBehaviour
{   
    public float speed = 5;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private float inputX, inputY;

    protected string horizontalAxis;
    protected string verticalAxis;

    private float stopX, stopY;

    public bool isFlipped => inputX < 0;

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        inputX = Input.GetAxisRaw(horizontalAxis);
        inputY = Input.GetAxisRaw(verticalAxis);
        Vector2 input = new Vector2(inputX, inputY).normalized;
        this.Flip();
        rigidbody.velocity = input * speed;
        //if (input != Vector2.zero) {
        //    animator.SetBool("isMoving", false);
        //    stopX = input.x;
        //    stopY = input.y;
        //}
        //else {
        //    animator.SetBool("isMoving", true);
        //}
        //animator.SetFloat("X", stopX);
        //animator.SetFloat("Y", stopY);

    }

    void Flip()
    {
        if (inputX <= 0.01f&&inputX>=-0.01f) return;
        if (inputX > 0)
        {
            transform.localScale = new Vector3(math.abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-math.abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    void doAttack()
    {
        
    }
}
