using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Image titleScreenImage;
    public Image fade;
    public float fadeOutTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOverlayIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeOverlayIn()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / 4)
        {
            titleScreenImage.color = new Color(1, 1, 1, i);
            yield return null;
        }

        StartCoroutine(FadeOverlayOut());
    }

    public IEnumerator FadeOverlayOut()
    {
        for (float i = 1; i > 0; i -= Time.deltaTime / 4)
        {
            titleScreenImage.color = new Color(1, 1, 1, i);
            yield return null;
        }

        StartCoroutine(FadeOverlayIn());
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        for (float i = 0; i < 1; i += Time.deltaTime *2)
        {
            fade.color = new Color(0, 0, 0, i);
            yield return null;
        }

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(1);
    }
}
