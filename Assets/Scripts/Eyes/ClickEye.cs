using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEye : BasicEye
{
    public override void OnLetGo()
    {

    }

    public override void OnTouch()
    {
        if (!closed && interactable)
        {
            Solve();
        }
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}
