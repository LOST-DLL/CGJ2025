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
    private Collider collider = new Collider();
    public Animator animator;
    private GameObject anim = null;

    public bool isHitBack = false;
    public bool isDead = false;
    public bool isMoving = false;
    
    private float moveX, moveY;
    private float currentHitTime = 0;
    private float hp = 0;
    public Vector3 moveVec = new Vector3();
    public Vector3 lastMoveVec = new Vector3();
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
        collider = this.gameObject.GetComponent<Collider>();
        this.hp = this.maxHp;
        this.anim = transform.Find("anim").gameObject;
    }

    public void doMove(float inputX,float inputY)
    {
        //TODO：这块因为不会用旧输入系统写的有点烂，看不顺眼可以改一下
        if (!this.isMoving)
        {
            if (inputX < 0.01 && inputX > -0.01 && inputY < 0.01 && inputY > -0.01)
            {
                rigidbody.velocity = Vector2.zero;
                return;
            }
            else
            {
                this.isMoving = true;
            }
        }
        if (inputX < 0.01 && inputX > -0.01 && inputY < 0.01 && inputY > -0.01)
        {
            lastMoveVec = moveVec;
            moveVec = Vector3.zero;
            rigidbody.velocity = Vector2.zero;
            this.isMoving = false;
            return;
        }
        this.moveX = inputX;
        this.moveY = inputY;
        moveVec = new Vector2(inputX, inputY).normalized;
        this.Flip();
        rigidbody.velocity = moveVec * moveSpeed;
    }
    void Flip()
    {
        if (moveX <= 0.01f&&moveX>=-0.01f) return;
        if (moveX > 0)
        {
            anim.transform.localScale = new Vector3(math.abs(anim.transform.localScale.x), anim.transform.localScale.y, anim.transform.localScale.z);
        }
        else
        {
            anim.transform.localScale = new Vector3(-math.abs(anim.transform.localScale.x), anim.transform.localScale.y, anim.transform.localScale.z);
        }
    }

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

    void useSkill()
    {
        
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

}
