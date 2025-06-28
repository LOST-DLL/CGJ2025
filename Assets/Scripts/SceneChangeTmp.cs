using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTmp : MonoBehaviour
{
    public GameObject[] player1Prefabs;  // �洢�������1��Ӧ��Ԥ�Ƽ�
    public GameObject[] player2Prefabs;  // �洢�������2��Ӧ��Ԥ�Ƽ�

    // ֱ���������1�����2�ĳ�����λ��
    public Transform player1SpawnPoint;  // ���1�ĳ�����
    public Transform player2SpawnPoint;  // ���2�ĳ�����

    private int player1Index;
    private int player2Index;

    void Start()
    {
        // �� PlayerPrefs �ж�ȡ����
        player1Index = PlayerPrefs.GetInt("Player1Index", 0);  // Ĭ��ֵΪ 0
        player2Index = PlayerPrefs.GetInt("Player2Index", 0);  // Ĭ��ֵΪ 0

        // ��������ʵ�������1�����2��Ԥ�Ƽ�
        InstantiateCharacter(player1Index, player1Prefabs, player1SpawnPoint);
        InstantiateCharacter(player2Index, player2Prefabs, player2SpawnPoint);
    }

    // ��������ʵ������ҵ�Ԥ�Ƽ�
    void InstantiateCharacter(int index, GameObject[] prefabs, Transform spawnPoint)
    {
        // ȷ��������Խ��
        if (index >= 0 && index < prefabs.Length)
        {
            // ʵ����Ԥ�Ƽ���ָ��λ��
            Instantiate(prefabs[index], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Index out of bounds for prefabs array: " + index);
        }
    }
}
