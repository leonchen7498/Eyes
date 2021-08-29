using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public TMP_Text Tmp;
    public float TransitionTime = 2;
    public string[] Dialogue;
    public Image cutsceneImage;

    private int count;
    private bool dialogueFinished;

    // Start is called before the first frame update
    void Start()
    {
        Tmp.text = Dialogue[count];
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueFinished && Input.GetMouseButtonUp(0))
        {
            count++;

            if (count >= Dialogue.Length)
            {
                dialogueFinished = true;
                Tmp.enabled = false;
                StartCoroutine(FadeOut());
                return;
            }

            Tmp.text = Dialogue[count];
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < TransitionTime)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(2);

        /*for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
        {
            cutsceneImage.color = new Color(0, 0, 0, i);
            yield return null;
        }*/
    }
}
