using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : PlayerControllerBase
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        horizontalAxis = "Player2Horizontal";
        verticalAxis = "Player2Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
