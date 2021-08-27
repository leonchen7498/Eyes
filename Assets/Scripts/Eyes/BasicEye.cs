using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEye : MonoBehaviour, Interactable
{
    public int eyesNeededToAppear = 0;
    [HideInInspector]
    public float timeNeededToAppear;

    protected bool interactable;

    private float distanceToMove = 0.3f;
    private Vector3 endPosition;

    public abstract void OnLetGo();
    public abstract void OnTouch();

    public virtual void Start()
    {
        if (eyesNeededToAppear > 0)
        {
            endPosition = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x - distanceToMove, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            interactable = true;
        }
    }

    public virtual void Update()
    {
    }

    public void Appear()
    {
        StartCoroutine(EyeAppear());
    }

    public IEnumerator EyeAppear()
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.localPosition;
        while (elapsedTime < timeNeededToAppear)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / timeNeededToAppear);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = endPosition;
        interactable = true;
    }
}
