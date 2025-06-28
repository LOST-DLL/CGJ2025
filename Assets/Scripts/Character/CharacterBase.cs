using Character.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public delegate void DamageDelegate(float damage);

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    public float curHp = 100;
    public event DamageDelegate TakeDamageEvent;


    [SerializeField] private float dashSpeed = 2000;
    [SerializeField] private float dashCD = 3;
    private float dashTimer = 0;
    [SerializeField] private float dashDamage = 5;
    [SerializeField] float dashMinSpeed = 1500;

    public Rigidbody2D rigidbody;
    private Collider2D collider;
    public Animator animator;
    private GameObject anim = null;

    public bool isDead = false;

    private float stopX;


    protected float currentSkillTime = 0;
    protected bool isDash = false;

    private void FixedUpdate() {
        if (dashTimer > 0) dashTimer -= Time.fixedDeltaTime;

        //if (isDash && rigidbody.velocity.magnitude < dashMinSpeed) {
        //    isDash = false;
        //}
    }

    public void Init()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        collider = this.gameObject.GetComponent<Collider2D>();
        this.curHp = this.maxHp;
        this.anim = transform.Find("anim").gameObject;
        TakeDamageEvent += OnTakeDamage;
    }

    public void Move(float inputX,float inputY)
    {
        Vector2 input = new Vector2(inputX, inputY).normalized;
        rigidbody.velocity = input * moveSpeed;

        if (input != Vector2.zero) {
            animator.SetBool("isMoving", true);
            stopX = inputX;
            if (stopX != 0) animator.SetFloat("X", stopX);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }

    public void Dash()
    {
        if (!this.isDash && dashTimer <= 0) {
            print("try Dash");
            rigidbody.AddForce(new Vector2(stopX, 0) * dashSpeed);
            //rigidbody.velocity = new Vector2(stopX, 0) * moveSpeed;
            dashTimer = dashCD;
            this.isDash = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (this.isDash) {
            var otherObject = collision.gameObject;
            print(otherObject.name);
            if (otherObject.CompareTag("Player")) {
                var otherCharacter = otherObject.GetComponent<CharacterBase>();
                TakeDamageEvent(dashDamage);
            }
        }
    }

    private void OnTakeDamage(float damage) {
        curHp -= damage;
        print($"curHp = {curHp}");
    }

    private void OnDestroy() {
        TakeDamageEvent -= OnTakeDamage;
    }
}
