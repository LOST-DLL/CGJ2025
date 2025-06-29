using Character.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterBase : MonoBehaviour {
    private int playerId;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    public float curHp = 100;

    public event Action<float> TakeDamageEvent;
    public event Action<Vector2> KnockbackEvent;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 40;
    private float dashCD = 0.5f;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private float dashDamage = 5;
    private float dashTimer = 0;
    private float outDashTimer = 0;
    private Vector2 dashDirection;
    protected bool isDash = false;

    [Header("Knockback Settings")]
    private float knockbackForce = 10f;
    private float backTime = 0.2f;
    private float backTimer = 0;
    private Vector2 backDirection;
    protected bool isBack = false;

    [Header("Components")]
    public Rigidbody2D rigidbody;
    private Collider2D collider;
    public Animator animator;
    private GameObject anim = null;

    private float stopX;
    private Vector2 lastMoveDirection = Vector2.right;

    private bool isDying = false;
    private float dieFallSpeed = 20;

    private float originFaceDir;

    private void FixedUpdate() {
        if (dashTimer > 0) dashTimer -= Time.fixedDeltaTime;

        if (isDash) {
            outDashTimer -= Time.fixedDeltaTime;
            rigidbody.velocity = dashDirection * dashSpeed;

            if (outDashTimer <= 0) {
                isDash = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        else if (isBack) {
            backTimer -= Time.fixedDeltaTime;
            rigidbody.velocity = backDirection * knockbackForce;

            if (backTimer <= 0) {
                isBack = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void Update() {
        if (isDying) {
            var pos = transform.position;
            pos.z += dieFallSpeed * Time.deltaTime;
            transform.position = pos;
        }

        Vector2 position = transform.position;
        float X = position.x;
        float Y = position.y;
        if (X < -9f || X > 9f || Y < -3f || Y > 10f) {
            if (!isDying) {
                Die();
            }
        }
    }

    public void Init(int playerId) {
        this.playerId = playerId;
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        collider = this.gameObject.GetComponent<Collider2D>();
        this.curHp = this.maxHp;
        this.anim = transform.Find("anim").gameObject;

        TakeDamageEvent += OnTakeDamage;
        if (playerId == 1) {
            TakeDamageEvent += HealthSystem.Instance.TakeDamage;
            HealthSystem.Instance.hitPoint = curHp;
            HealthSystem.Instance.maxHitPoint = maxHp;
            this.originFaceDir = 1f;
        }
        else {
            TakeDamageEvent += HealthSystem.Instance.UseMana;
            HealthSystem.Instance.manaPoint = curHp;
            HealthSystem.Instance.maxManaPoint = maxHp;
            this.originFaceDir = -1f;
        }
        this.stopX = originFaceDir;
        animator.SetFloat("X", stopX);

        HealthSystem.Instance.PlayerDieEvent += DisableController;
        HealthSystem.Instance.ResetLevelEvent += DestroySelf;
        KnockbackEvent += OnKnockback;
    }

    public void Move(float inputX, float inputY) {
        if (isDash || isBack) return;

        Vector2 input = new Vector2(inputX, inputY).normalized;
        rigidbody.velocity = input * moveSpeed;

        if (input != Vector2.zero) {
            lastMoveDirection = input;
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
            dashDirection = lastMoveDirection.normalized;
            if (dashDirection == Vector2.zero) dashDirection = new Vector2(stopX, 0).normalized;
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
                    otherCharacter.TakeDamageEvent?.Invoke(dashDamage);
                    otherCharacter.KnockbackEvent?.Invoke(dashDirection);

                    this.isDash = false;
                    rigidbody.velocity = Vector2.zero;
                }
            }
        }
    }

    private void OnTakeDamage(float damage) {
        curHp -= damage;
        if (curHp <= 0) {
            Die();
        }
        //print($"{gameObject.name} curHp = {curHp}");

        isDash = false;
        outDashTimer = 0;
    }

    private void OnKnockback(Vector2 direction) {
        isDash = false;
        outDashTimer = 0;

        backDirection = direction.normalized;
        backTimer = backTime;
        isBack = true;
    }

    public void Die() {
        isDying = true;
        HealthSystem.Instance.LevelEnd();

        if (collider != null) collider.enabled = false;
        if (rigidbody != null) rigidbody.simulated = false;
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

    private void DisableController() {
        if (playerId == 1) {
            GetComponent<Player1Controller>().enabled = false;
        }
        else {
            GetComponent<Player2Controller>().enabled = false;
        }
    }

    private void OnDestroy() {
        TakeDamageEvent -= OnTakeDamage;
        KnockbackEvent -= OnKnockback;
        if (originFaceDir > 0) {
            TakeDamageEvent -= HealthSystem.Instance.TakeDamage;
        }
        else {
            TakeDamageEvent -= HealthSystem.Instance.UseMana;
        }
        HealthSystem.Instance.PlayerDieEvent -= DisableController;
        HealthSystem.Instance.ResetLevelEvent -= DestroySelf;

    }

}
