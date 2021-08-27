using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEye : BasicEye
{
    private Animator animator;

    private float blinkTimer;
    private float blinkInterval;

    private bool closed;

    public override void OnLetGo()
    {

    }

    public override void OnTouch()
    {
        if (!closed && interactable)
        {
            animator.SetBool(Parameters.Closed, true);
            closed = true;
            GameManager.Instance.EyeSolved();
        }
    }

    public override void Start()
    {
        base.Start();
        animator = this.GetComponent<Animator>();
        blinkInterval = Random.Range(4f, 8f);
    }

    public override void Update()
    {
        base.Update();
        if (closed)
        {
            return;
        }

        blinkTimer += Time.deltaTime;

        if (blinkTimer >= blinkInterval)
        {
            animator.Play(States.Blinking);
            blinkTimer = 0f;
        }
    }
}
