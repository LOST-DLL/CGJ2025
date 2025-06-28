using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllerBase : MonoBehaviour
{   
    private float inputX, inputY;

    protected float originFaceDir;
    protected string horizontalAxis;
    protected string verticalAxis;

    public KeyCode dashKey;

    public string characterName;

    public bool enablePlayerInput = false;

    private CharacterBase selectedCharacter = null;

    protected void Start()
    {
        this.LoadCharacter(characterName);
    }
    
    protected void Update()
    {
        if (this.enablePlayerInput)
        {
            inputX = Input.GetAxisRaw(horizontalAxis);
            inputY = Input.GetAxisRaw(verticalAxis);
        }
        if (this.selectedCharacter)
        {
            this.selectedCharacter.Move(inputX, inputY);
            if (Input.GetKey(dashKey)) {
                this.selectedCharacter.Dash();
            }
        }
    }

    void LoadCharacter(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Character/" + name);

        if (prefab != null)
        {
            var go = Instantiate(prefab, transform.position, Quaternion.identity);
            go.transform.SetParent(this.transform);
            this.SetSelectedCharacter(go.GetComponent<CharacterBase>());
        }
        else
        {
            Debug.LogError("预制体不存在: " + name);
        }
    }

    void SetSelectedCharacter(CharacterBase character)
    {
        this.enablePlayerInput = true;
        this.selectedCharacter = character;
        this.selectedCharacter.Init(originFaceDir);
    }
}
