using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGrid : MonoBehaviour
{
    public GridLayoutGroup characterGrid;  // ��ɫͷ������
    public Image player1SelectionBox;     // ���1��ѡ�п�
    public Image player2SelectionBox;     // ���2��ѡ�п�
    public Image[] characterSprites;     // ��ɫͷ��ͼ��
    public Image player1Image;            // ���1���Ͻ�ͷ����ʾ
    public Image player2Image;            // ���2���Ͻ�ͷ����ʾ

    private int player1Index = 0;         // ���1��ǰѡ��Ľ�ɫ����
    private int player2Index = 0;         // ���2��ǰѡ��Ľ�ɫ����
    private int gridSize;                 // �����������

    void Awake()
    {
        gridSize = characterSprites.Length;  // ��ȡ�����������
        UpdateSelectionBox();                // ��ʼ��ʱ����ѡ�п�λ��
    }

    void Start()
    {
        gridSize = characterSprites.Length;  // ��ȡ�����������
        UpdateSelectionBox();                // ��ʼ��ʱ����ѡ�п�λ��
    }

    void Update()
    {
        // ���1�����루WASD��
        if (Input.GetKeyDown(KeyCode.W)) MovePlayer1Up();
        if (Input.GetKeyDown(KeyCode.S)) MovePlayer1Down();
        if (Input.GetKeyDown(KeyCode.A)) MovePlayer1Left();
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer1Right();

        // ���2�����루�������ң�
        if (Input.GetKeyDown(KeyCode.UpArrow)) MovePlayer2Up();
        if (Input.GetKeyDown(KeyCode.DownArrow)) MovePlayer2Down();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MovePlayer2Left();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MovePlayer2Right();
        //�ȴ��Ż�
        UpdateSelectionBox();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ������һ���������������ƿ��Ը�����Ҫ�滻
            LoadNextScene();
        }
    }

    void MovePlayer1Up()
    {
        player1Index = (player1Index - 1 + gridSize) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer1Down()
    {
        player1Index = (player1Index + 1) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer1Left()
    {
        player1Index = (player1Index - 1 + gridSize) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer1Right()
    {
        player1Index = (player1Index + 1) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer2Up()
    {
        player2Index = (player2Index - 1 + gridSize) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer2Down()
    {
        player2Index = (player2Index + 1) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer2Left()
    {
        player2Index = (player2Index - 1 + gridSize) % gridSize;
        UpdateSelectionBox();
    }

    void MovePlayer2Right()
    {
        player2Index = (player2Index + 1) % gridSize;
        UpdateSelectionBox();
    }

    void UpdateSelectionBox()
    {
        // ����ÿ�����ѡ��Ľ�ɫ�������е�λ��
        RectTransform player1Rect = characterGrid.transform.GetChild(player1Index).GetComponent<RectTransform>();
        RectTransform player2Rect = characterGrid.transform.GetChild(player2Index).GetComponent<RectTransform>();

        // �������1��ѡ�п�λ��
        player1SelectionBox.rectTransform.position = player1Rect.position;
        // �������2��ѡ�п�λ��
        player2SelectionBox.rectTransform.position = player2Rect.position;

        player1Image.sprite = characterSprites[player1Index].sprite;  // ��ȡImage��sprite
        player2Image.sprite = characterSprites[player2Index].sprite;  // ��ȡImage��sprite
    }
    void LoadNextScene()
    {
        string nextSceneName = "SampleScene";  
        SceneManager.LoadScene(nextSceneName);
    }
}
