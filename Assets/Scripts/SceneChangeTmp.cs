using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTmp : MonoBehaviour
{
    public GameObject[] player1Prefabs;  // 存储所有玩家1对应的预制件
    public GameObject[] player2Prefabs;  // 存储所有玩家2对应的预制件

    // 直接引用玩家1和玩家2的控制器
    public PlayerStart player1Start;  // 玩家1的出生点
    public PlayerStart player2Start;  // 玩家2的出生点

    private int player1Index;
    private int player2Index;


    void Awake()
    {
        // 从 PlayerPrefs 中读取索引
        player1Index = PlayerPrefs.GetInt("Player1Index", 0);  // 默认值为 0
        player2Index = PlayerPrefs.GetInt("Player2Index", 0);  // 默认值为 0

        // 根据索引实例化玩家1和玩家2的预制件
        InstantiateCharacter(player1Index, player1Prefabs, player1Start);
        InstantiateCharacter(player2Index, player2Prefabs, player2Start);
    }
    void Start()
    {
        
    }

    // 根据索引实例化玩家的预制件
    void InstantiateCharacter(int index, GameObject[] prefabs, PlayerStart player)
    {
        // 确保索引不越界
        if (index >= 0 && index < prefabs.Length)
        {
            Debug.Log("name is: " + prefabs[index].name);
            // 获取预制件的名称并赋值给玩家的 Character 属性
            string prefabName = prefabs[index].name;  // 获取预制件的名称

            // 将名称赋值给玩家的 characterName 属性
            player.characterName = prefabName;
        }
        else
        {
            Debug.LogWarning("Index out of bounds for prefabs array: " + index);
        }
    }
}
