using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PlayerControllerBase
{
    // Start is called before the first frame update
    private void Awake() {
        horizontalAxis = "Player1Horizontal";
        verticalAxis = "Player1Vertical";
        originFaceDir = 1f;
        dashKey = KeyCode.J;
    }
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
