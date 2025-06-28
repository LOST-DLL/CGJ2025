using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PlayerControllerBase
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        horizontalAxis = "Player1Horizontal";
        verticalAxis = "Player1Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
