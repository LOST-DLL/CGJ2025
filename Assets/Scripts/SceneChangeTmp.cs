using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTmp : MonoBehaviour
{
    public GameObject[] player1Prefabs;  // 存储所有玩家1对应的预制件
    public GameObject[] player2Prefabs;  // 存储所有玩家2对应的预制件

    // 直接引用玩家1和玩家2的出生点位置
    public Transform player1SpawnPoint;  // 玩家1的出生点
    public Transform player2SpawnPoint;  // 玩家2的出生点

    private int player1Index;
    private int player2Index;

    void Start()
    {
        // 从 PlayerPrefs 中读取索引
        player1Index = PlayerPrefs.GetInt("Player1Index", 0);  // 默认值为 0
        player2Index = PlayerPrefs.GetInt("Player2Index", 0);  // 默认值为 0

        // 根据索引实例化玩家1和玩家2的预制件
        InstantiateCharacter(player1Index, player1Prefabs, player1SpawnPoint);
        InstantiateCharacter(player2Index, player2Prefabs, player2SpawnPoint);
    }

    // 根据索引实例化玩家的预制件
    void InstantiateCharacter(int index, GameObject[] prefabs, Transform spawnPoint)
    {
        // 确保索引不越界
        if (index >= 0 && index < prefabs.Length)
        {
            // 实例化预制件到指定位置
            Instantiate(prefabs[index], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Index out of bounds for prefabs array: " + index);
        }
    }
}
