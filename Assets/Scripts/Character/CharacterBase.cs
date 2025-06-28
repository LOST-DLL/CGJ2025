using System;
using System.Collections;
using System.Collections.Generic;
using Character.Manager;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    [SerializeField,Range(0,10)] private float weight = 1;//用于计算击退距离，重量越大越不易被击退
    [SerializeField] private float hitBackSpeed = 20;
    [SerializeField] private float maxHitTime = 10;//最大的击退时间
    
    
    public Rigidbody2D rigidbody = new Rigidbody2D();
    private Collider2D collider = new Collider2D();
    public Animator animator;
    private GameObject anim = null;

    public bool isHitBack = false;
    public bool isDead = false;
    public bool isMoving = false;
    
    private float moveX, moveY;
    private float stopX, stopY;
    private float currentHitTime = 0;
    private float hp = 0;
    public Vector3 moveVec = new Vector3();
    public Vector3 lastMoveVec = new Vector3();

    [Header("---- Skill ----")]
    [SerializeField] protected float skillTime = 10;

    [SerializeField] protected float dashSpeed = 50;

    [SerializeField] protected float dashDamageRadius = 5;//冲刺时能够造成伤害的半径

    protected float currentSkillTime = 0;
    protected bool isDash = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        CharacterManager.Instance.AddCharacter(this);
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        collider = this.gameObject.GetComponent<Collider2D>();
        this.hp = this.maxHp;
        this.anim = transform.Find("anim").gameObject;
    }

    public void doMove(float inputX,float inputY)
    {
        Vector2 input = new Vector2(inputX, inputY).normalized;
        rigidbody.velocity = input * moveSpeed;

        if (input != Vector2.zero) {
            animator.SetBool("isMoving", true);
            stopX = inputX;
            stopY = inputY;
            if (stopX != 0) animator.SetFloat("X", stopX);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }
    //void Flip()
    //{
    //    if (moveX <= 0.01f&&moveX>=-0.01f) return;
    //    if (moveX > 0)
    //    {
    //        anim.transform.localScale = new Vector3(math.abs(anim.transform.localScale.x), anim.transform.localScale.y, anim.transform.localScale.z);
    //    }
    //    else
    //    {
    //        anim.transform.localScale = new Vector3(-math.abs(anim.transform.localScale.x), anim.transform.localScale.y, anim.transform.localScale.z);
    //    }
    //}

    public void onHit(CharacterBase character)
    {
        if (!isHitBack)
        {
            StartCoroutine(nameof(HitBackCoroutine));
        }

        this.hp -= character.attackPower;

        if (hp <= 0)
        {
            doDie();
            this.hp = 0;
        }
    }

    public void useDash()
    {
        if (!this.isDash) {
            StartCoroutine(nameof(DashCoroutine));
        }
    }

    void doDie()
    {
        this.isDead = true;
    }

    IEnumerator HitBackCoroutine()
    {
        isHitBack = true;
        collider.isTrigger = true;
        currentHitTime = 0;
        while (currentHitTime < maxHitTime)
        {
            currentHitTime += Time.deltaTime;
            yield return null;
        }

        collider.isTrigger = false;
        isHitBack = false;
    }

    IEnumerator DashCoroutine() {
        isDash = true;
        currentSkillTime = 0;

        while (currentSkillTime < skillTime) {
            if (this.isMoving) {
                this.rigidbody.velocity = moveVec * dashSpeed * Time.deltaTime;
            }
            else {
                this.rigidbody.velocity = lastMoveVec * dashSpeed * Time.deltaTime;
            }

            foreach (var character in CharacterManager.Instance.characterList) {
                if (character != this) {
                    float distance = Vector3.Distance(transform.position, character.transform.position);
                    if (distance <= dashDamageRadius) {
                        character.onHit(this);
                    }
                }
            }
            yield return null;
        }
    }
}
