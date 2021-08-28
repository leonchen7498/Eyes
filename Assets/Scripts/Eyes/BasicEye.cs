using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEye : MonoBehaviour, Interactable
{
    public int eyesNeededToAppear = 0;
    [HideInInspector]
    public float timeNeededToAppear;

    [Header("For upside down eyes")]
    public bool needToBeUpsideDownToSolve;
    public SkinnedMeshRenderer skinnedMeshRenderOfEyeball;
    public Material WhitePupil;
    private Material originalPupil;
    private Material[] materials;

    protected Animator animator;
    protected bool interactable;
    protected bool closed;

    protected float blinkTimer;
    protected float blinkInterval;

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

        animator = GetComponent<Animator>();
        blinkInterval = Random.Range(4f, 8f);

        if (needToBeUpsideDownToSolve)
        {
            originalPupil = skinnedMeshRenderOfEyeball.materials[2];
            materials = skinnedMeshRenderOfEyeball.materials;
            materials[2] = WhitePupil;

            skinnedMeshRenderOfEyeball.materials = materials;
            interactable = false;
        }
    }

    public virtual void Update()
    {
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

        if (needToBeUpsideDownToSolve)
        { 
            Vector3 sphereRotation = GameManager.Instance.sphere.transform.rotation.eulerAngles;
            if ((sphereRotation.x > 345 || sphereRotation.x < 15) && sphereRotation.z > 165 && sphereRotation.z < 195)
            {
                materials[2] = originalPupil;
                interactable = true;
            }
            else
            {
                materials[2] = WhitePupil;
                interactable = false;
            }
            skinnedMeshRenderOfEyeball.materials = materials;
        }
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

    public void Solve()
    {
        animator.SetBool(Parameters.Closed, true);
        closed = true;
        GameManager.Instance.EyeSolved();
    }
}
