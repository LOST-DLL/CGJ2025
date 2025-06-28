using System;
using System.Collections;
using Character.Manager;
using UnityEngine;

namespace Character
{
    public class Potato:CharacterBase
    {
        [Header("---- Skill ----")]
        [SerializeField] private float skillTime = 10;

        [SerializeField] private float dashSpeed = 50;

        [SerializeField] private float dashDamageRadius = 5;//冲刺时能够造成伤害的半径

        private float currentSkillTime = 0;
        private bool isDash = false;
        void useSkill()
        {
            if (!this.isDash)
            {
                StartCoroutine(nameof(DashCoroutine));
            }
        }

        IEnumerator DashCoroutine()
        {
            isDash = true;
            currentSkillTime = 0;

            while (currentSkillTime < skillTime)
            {
                if (this.isMoving)
                {
                    this.rigidbody.velocity = moveVec * dashSpeed * Time.deltaTime;
                }
                else
                {
                    this.rigidbody.velocity = lastMoveVec * dashSpeed * Time.deltaTime;
                }
                
                foreach (var character in CharacterManager.Instance.characterList)
                {
                    if (character != this)
                    {
                        float distance = Vector3.Distance(transform.position, character.transform.position);
                        if (distance <= dashDamageRadius)
                        {
                            character.onHit(this);
                        }
                    }
                }
                yield return null;
            }
        }
    }
}