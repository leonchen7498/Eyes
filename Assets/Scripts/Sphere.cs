using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour, Interactable
{
    public int RotationSpeed = 100;

    private bool allowRotation = false;

    void Start()
    {
    }

    public void OnLetGo()
    {
        allowRotation = false;    
    }

    public void OnTouch()
    {
        allowRotation = true;
    }

    void Update()
    {
        if (allowRotation)
        {
            float rotX = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * RotationSpeed * Mathf.Deg2Rad;

            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
    }
}
