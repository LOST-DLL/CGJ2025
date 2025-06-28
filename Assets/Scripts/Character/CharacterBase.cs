using Character.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamageDelegate(float damage, Vector2 direction);

public class CharacterBase : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    public float curHp = 100;

    public event DamageDelegate TakeDamageEvent;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 40;
    private float dashCD = 0.25f;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private float dashDamage = 5;
    private float dashTimer = 0;
    private float outDashTimer = 0;
    private Vector2 dashDirection;
    protected bool isDash = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 50;
    [SerializeField] private float backTime = 1f;
    private float backTimer = 0;
    private Vector2 backDirection;
    protected bool isBack = false;

    [Header("Components")]
    public Rigidbody2D rigidbody;
    private Collider2D collider;
    public Animator animator;
    private GameObject anim = null;

    private float stopX;
    public bool isDead = false;

    private void FixedUpdate() {
        if (dashTimer > 0) dashTimer -= Time.fixedDeltaTime;

        if (isDash) {
            outDashTimer -= Time.fixedDeltaTime;

            // 平滑推进 dash
            rigidbody.velocity = dashDirection * dashSpeed;

            if (outDashTimer <= 0) {
                isDash = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        else if (isBack) {
            backTimer -= Time.fixedDeltaTime;

            // 平滑推进 knockback
            rigidbody.velocity = backDirection * knockbackForce;

            if (backTimer <= 0) {
                isBack = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void Init(float originFaceDir) {
        this.stopX = originFaceDir;
        animator.SetFloat("X", stopX);
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        collider = this.gameObject.GetComponent<Collider2D>();
        this.curHp = this.maxHp;
        this.anim = transform.Find("anim").gameObject;
        TakeDamageEvent += OnTakeDamage;
    }

    public void Move(float inputX, float inputY) {
        if (isDash || isBack) return;  // 在Dash或击退时不能自己动

        Vector2 input = new Vector2(inputX, inputY).normalized;
        rigidbody.velocity = input * moveSpeed;

        if (input != Vector2.zero) {
            animator.SetBool("isMoving", true);
            if (inputX != 0) stopX = inputX;
            animator.SetFloat("X", stopX);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }

    public void Dash() {
        if (!this.isDash && dashTimer <= 0 && !this.isBack) {
            dashDirection = new Vector2(stopX, 0).normalized;
            if (dashDirection == Vector2.zero) dashDirection = Vector2.right;

            dashTimer = dashCD;
            this.isDash = true;
            outDashTimer = dashTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (this.isDash) {
            var otherObject = collision.gameObject;
            if (otherObject.CompareTag("Player")) {
                var otherCharacter = otherObject.GetComponent<CharacterBase>();
                if (otherCharacter != null) {
                    otherCharacter.TakeDamageEvent?.Invoke(dashDamage, dashDirection);
                    this.isDash = false;
                    rigidbody.velocity = Vector2.zero;
                }
            }
        }
    }

    private void OnTakeDamage(float damage, Vector2 direction) {
        curHp -= damage;
        print($"{gameObject.name} curHp = {curHp}");

        // 打断 Dash
        isDash = false;
        outDashTimer = 0;

        // 击退设置
        backDirection = direction.normalized;
        backTimer = backTime;
        isBack = true;
    }

    private void OnDestroy() {
        TakeDamageEvent -= OnTakeDamage;
    }
}
