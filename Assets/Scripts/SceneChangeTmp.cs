using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTmp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int player1Index = PlayerPrefs.GetInt("Player1Index", 0);  // �ڶ���������Ĭ��ֵ�����û���ҵ������ֵ��ʹ��0
        int player2Index = PlayerPrefs.GetInt("Player2Index", 0);

        // ��ӡ player1Index��ȷ����ȡ��ȷ
        Debug.Log("Player 1's index in this scene: " + player1Index);
        Debug.Log("Player 2's index in this scene: " + player2Index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
