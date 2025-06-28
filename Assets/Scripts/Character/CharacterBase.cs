using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float weight = 10;//用于计算击退距离，重量越大越不易被击退
    
    private float moveX, moveY;
    private Rigidbody2D rigidbody = new Rigidbody2D();
    public Animator animator;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public void doMove(float inputX,float inputY)
    {
        this.moveX = inputX;
        this.moveY = inputY;
        Vector2 moveVec = new Vector2(inputX, inputY).normalized;
        this.Flip();
        rigidbody.velocity = moveVec * moveSpeed;
    }
    void Flip()
    {
        if (moveX <= 0.01f&&moveX>=-0.01f) return;
        if (moveX > 0)
        {
            transform.localScale = new Vector3(math.abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-math.abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

}
