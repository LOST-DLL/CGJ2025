using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float attackPower = 10;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float weight = 10;//用于计算击退距离，重量越大越不易被击退
    
    

}
