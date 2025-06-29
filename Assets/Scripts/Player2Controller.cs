using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : PlayerControllerBase
{
    private void Awake() {
        horizontalAxis = "Player2Horizontal";
        verticalAxis = "Player2Vertical";
        originFaceDir = -1f;
        dashKey = KeyCode.Keypad2;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
