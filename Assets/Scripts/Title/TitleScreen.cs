using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Image titleScreenImage;
    public float fadeOutTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        for (float i = 1; i >= 0; i -= Time.deltaTime *2)
        {
            titleScreenImage.color = new Color(i, i, i, 1);
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
