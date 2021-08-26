using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject SphereObject;

    private Interactable Sphere;
    private Interactable touchedObject;

    void Start()
    {
        touchedObject = null;
        Sphere = SphereObject.GetComponent<Interactable>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && touchedObject != null)
        {
            touchedObject.OnLetGo();
            touchedObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (touchedObject == null && Physics.Raycast(ray, out hit))
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
}
