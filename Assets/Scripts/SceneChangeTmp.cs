using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTmp : MonoBehaviour
{
    public GameObject[] player1Prefabs;  // �洢�������1��Ӧ��Ԥ�Ƽ�
    public GameObject[] player2Prefabs;  // �洢�������2��Ӧ��Ԥ�Ƽ�

    // ֱ���������1�����2�Ŀ�����
    public PlayerStart player1Start;  // ���1�ĳ�����
    public PlayerStart player2Start;  // ���2�ĳ�����

    private int player1Index;
    private int player2Index;


    void Awake()
    {
        // �� PlayerPrefs �ж�ȡ����
        player1Index = PlayerPrefs.GetInt("Player1Index", 0);  // Ĭ��ֵΪ 0
        player2Index = PlayerPrefs.GetInt("Player2Index", 0);  // Ĭ��ֵΪ 0

        // ��������ʵ�������1�����2��Ԥ�Ƽ�
        InstantiateCharacter(player1Index, player1Prefabs, player1Start);
        InstantiateCharacter(player2Index, player2Prefabs, player2Start);
    }
    void Start()
    {
        
    }

    // ��������ʵ������ҵ�Ԥ�Ƽ�
    void InstantiateCharacter(int index, GameObject[] prefabs, PlayerStart player)
    {
        // ȷ��������Խ��
        if (index >= 0 && index < prefabs.Length)
        {
            Debug.Log("name is: " + prefabs[index].name);
            // ��ȡԤ�Ƽ������Ʋ���ֵ����ҵ� Character ����
            string prefabName = prefabs[index].name;  // ��ȡԤ�Ƽ�������

            // �����Ƹ�ֵ����ҵ� characterName ����
            player.characterName = prefabName;
        }
        else
        {
            Debug.LogWarning("Index out of bounds for prefabs array: " + index);
        }
    }
}
