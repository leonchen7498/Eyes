using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderEye : BasicEye
{
    public OrderEye PreviousEye;
    public OrderEye NextEye;

    public override void OnLetGo()
    {

    }

    public override void OnTouch()
    {
        if (closed || !interactable)
        {
            return;
        }

        if (GameManager.Instance.eyesClicked.Any())
        {
            audio.Play();
            if (GameManager.Instance.eyesClicked.Last() == PreviousEye)
            {
                animator.SetBool(Parameters.Closed, true);
                closed = true;
                GameManager.Instance.eyesClicked.Add(this);
            }
            else
            {
                GameManager.Instance.eyesClicked.First().ResetEyes();
                GameManager.Instance.eyesClicked.First().SetBlinkInterval(0f);
                GameManager.Instance.eyesClicked = new List<OrderEye>();
            }

            if (NextEye == null)
            {
                GameManager.Instance.eyesClicked = new List<OrderEye>();
                GameManager.Instance.EyeSolved();

                if (!string.IsNullOrEmpty(text))
                {
                    GameManager.Instance.ShowText(text, durationOfText);
                }
            }
        }
        else
        {
            if (PreviousEye == null)
            {
                audio.Play();
                animator.SetBool(Parameters.Closed, true);
                closed = true;
                GameManager.Instance.eyesClicked.Add(this);
            }
        }
    }

    void Start()
    {
        base.Start();

        if (PreviousEye == null)
        {
            StartCoroutine(WaitAMoment());
        }
    }

    void Update()
    {
        base.Update();
    }

    public void ResetEyes()
    {
        closed = false;
        animator.SetBool(Parameters.Closed, false);

        if (NextEye != null)
        {
            NextEye.ResetEyes();
        }
    }

    public void SetBlinkInterval(float interval)
    {
        blinkTimer = 0f;
        blinkInterval = 5f;
        delayBeforeFirstBlink = interval;

        if (NextEye != null)
        {
            NextEye.SetBlinkInterval(interval + 0.3f);
        }
    }

    public IEnumerator WaitAMoment()
    {
        float elapsedTime = 0;

        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SetBlinkInterval(0f);
    }
}
