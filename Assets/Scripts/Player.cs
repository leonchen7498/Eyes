using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject SphereObject;

    private Interactable Sphere;
    private Interactable touchedObject;

    private bool enabled;

    void Start()
    {
        touchedObject = null;
        Sphere = SphereObject.GetComponent<Interactable>();
        enabled = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && touchedObject != null)
        {
            if (touchedObject == Sphere)
            {
                touchedObject.OnLetGo();
                touchedObject = null;
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == Tag.Interact)
                {
                    hit.transform.gameObject.GetComponent<Interactable>().OnLetGo();
                }
            }

            GameManager.Instance.particleEffect = false;
            touchedObject = null;
        }

        if (!enabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.heldEye = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == Tag.Interact)
                {
                    touchedObject = hit.transform.gameObject.GetComponent<Interactable>();
                    touchedObject.OnTouch();
                }
            }
            else
            {
                touchedObject = Sphere;
                touchedObject.OnTouch();
            }
        }
    }

    public void ToggleControls()
    {
        enabled = !enabled;
    }
}
