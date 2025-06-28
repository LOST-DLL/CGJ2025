using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGrid : MonoBehaviour
{
    public GridLayoutGroup characterGrid;  // 角色头像网格
    public Image player1SelectionBox;     // 玩家1的选中框
    public Image player2SelectionBox;     // 玩家2的选中框
    public Image[] characterSprites;     // 角色头像图像
    public Image player1Image;            // 玩家1左上角头像显示
    public Image player2Image;            // 玩家2右上角头像显示

    private int player1Index = 0;         // 玩家1当前选择的角色索引
    private int player2Index = 0;         // 玩家2当前选择的角色索引
    private int gridSize;                 // 网格的总项数

    void Awake()
    {
        gridSize = characterSprites.Length;  // 获取网格的总项数
        UpdateSelectionBox();                // 初始化时更新选中框位置
    }

    void Start()
    {
        gridSize = characterSprites.Length;  // 获取网格的总项数
        UpdateSelectionBox();                // 初始化时更新选中框位置
    }

    void Update()
    {
        // 玩家1的输入（WASD）
        if (Input.GetKeyDown(KeyCode.W)) MovePlayer1Up();
        if (Input.GetKeyDown(KeyCode.S)) MovePlayer1Down();
        if (Input.GetKeyDown(KeyCode.A)) MovePlayer1Left();
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer1Right();

        // 玩家2的输入（上下左右）
        if (Input.GetKeyDown(KeyCode.UpArrow)) MovePlayer2Up();
        if (Input.GetKeyDown(KeyCode.DownArrow)) MovePlayer2Down();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MovePlayer2Left();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MovePlayer2Right();
        //等待优化
        UpdateSelectionBox();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 加载下一个场景，场景名称可以根据需要替换
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
        // 计算每个玩家选择的角色在网格中的位置
        RectTransform player1Rect = characterGrid.transform.GetChild(player1Index).GetComponent<RectTransform>();
        RectTransform player2Rect = characterGrid.transform.GetChild(player2Index).GetComponent<RectTransform>();

        // 更新玩家1的选中框位置
        player1SelectionBox.rectTransform.position = player1Rect.position;
        // 更新玩家2的选中框位置
        player2SelectionBox.rectTransform.position = player2Rect.position;

        player1Image.sprite = characterSprites[player1Index].sprite;  // 获取Image的sprite
        player2Image.sprite = characterSprites[player2Index].sprite;  // 获取Image的sprite
    }
    void LoadNextScene()
    {
        string nextSceneName = "SampleScene";  
        SceneManager.LoadScene(nextSceneName);
    }
}
