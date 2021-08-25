using System.Collections;
using UnityEngine;

public class ToonEyes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Perspective camera:
        // Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;

        // transform.LookAt(mouseRay.origin + mouseRay.direction * midPoint);

        // Orthographic camera:
        transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f)));
    }
}
