using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllerBase : MonoBehaviour
{   
    private float inputX, inputY;

    protected float originFaceDir;
    protected string horizontalAxis;
    protected string verticalAxis;

    public KeyCode dashKey;

    public CharacterBase character;

    protected void Start()
    {
        character = GetComponent<CharacterBase>();
    }
    
    protected void Update()
    {
        inputX = Input.GetAxisRaw(horizontalAxis);
        inputY = Input.GetAxisRaw(verticalAxis);
        this.character.Move(inputX, inputY);
        if (Input.GetKey(dashKey)) {
            this.character.Dash();
        }
    }
}
