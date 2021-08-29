using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEye : BasicEye
{
    public DrawEye EyeToConnectTo;

    public override void OnLetGo()
    {
        if (!closed && interactable)
        {
            if (GameManager.Instance.heldEye != null && GameManager.Instance.heldEye == EyeToConnectTo)
            {
                Solve();
                audio.Play();
                GameManager.Instance.heldEye.Solve();
                if (!string.IsNullOrEmpty(text))
                {
                    GameManager.Instance.ShowText(text, durationOfText);
                }
            }
        }
    }

    public override void OnTouch()
    {
        if (!closed && interactable)
        {
            GameManager.Instance.heldEye = this;
            GameManager.Instance.particleEffect = true;
        }
    }

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
