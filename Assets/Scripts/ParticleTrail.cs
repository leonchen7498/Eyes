using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrail : MonoBehaviour
{
    public ParticleSystem particles;

    public void InstantiateAndPlay(Vector3 position, float destroyDelay)
    {
        ParticleSystem ps = Instantiate(particles, position, Quaternion.identity) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, destroyDelay);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.particleEffect)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1.5f;
            InstantiateAndPlay(GetComponent<Camera>().ScreenToWorldPoint(mousePos), 1f);
        }
    }
}
