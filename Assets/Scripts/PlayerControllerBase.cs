using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllerBase : MonoBehaviour
{   
    private float inputX, inputY;

    public string Character;

    protected string horizontalAxis;
    protected string verticalAxis;

    public bool isFlipped => inputX < 0;

    public bool isMoving = true;

    private CharacterBase selectedCharacter = null;

    protected void Start()
    {
        
        this.loadCharacter(Character);
    }
    
    protected void Update()
    {
        inputX = Input.GetAxisRaw(horizontalAxis);
        inputY = Input.GetAxisRaw(verticalAxis);
        if (this.isMoving&&this.selectedCharacter)
        {
            this.selectedCharacter.doMove(inputX, inputY);
        }
    }

    void loadCharacter(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Character/" + name);

        if (prefab != null)
        {
            var go = Instantiate(prefab, transform.position, Quaternion.identity);
            go.transform.SetParent(this.transform);
            this.setSelectedCharacter(go.GetComponent<CharacterBase>());
        }
        else
        {
            Debug.LogError("预制体不存在: " + name);
        }
    }

    void setSelectedCharacter(CharacterBase character)
    {
        this.selectedCharacter = character;
        this.selectedCharacter.Init();
    }
}
