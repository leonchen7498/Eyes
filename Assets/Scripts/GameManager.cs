using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public float TimeNeededToShowEye = 8f;
    public TMP_Text textObject;
    public Image fadeObject;
    public Player playerObject;

    [HideInInspector]
    public DrawEye heldEye;
    [HideInInspector]
    public List<OrderEye> eyesClicked;
    [HideInInspector]
    public bool particleEffect;
    public Sphere sphere;

    private List<BasicEye> eyes;
    private int AmountOfEyesSolved;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tag.Interact);

        eyes = new List<BasicEye>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            BasicEye eye = gameObjects[i].GetComponent<BasicEye>();
            eye.timeNeededToAppear = TimeNeededToShowEye;
            eyes.Add(eye);
        }

        eyesClicked = new List<OrderEye>();
    }

    void Update()
    {

    }

    public void EyeSolved()
    {
        AmountOfEyesSolved++;
        foreach (BasicEye eye in eyes)
        {
            if (AmountOfEyesSolved == eye.eyesNeededToAppear)
            {
                eye.Appear();
            }
        }

        if (AmountOfEyesSolved == 3)
        {
            StartCoroutine(Final());
        }
    }

    public void ShowText(string text, int seconds)
    {
        StartCoroutine(ShowAndHideText(text, seconds));
    }

    public IEnumerator ShowAndHideText(string text, int seconds)
    {
        playerObject.ToggleControls();

        for (float i = 0; i < 0.4f; i += Time.deltaTime * 2)
        {
            fadeObject.color = new Color(0, 0, 0, i);
            yield return null;
        }

        textObject.text = text;

        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        textObject.text = "";

        for (float i = 0.4f; i > 0; i -= Time.deltaTime * 2)
        {
            fadeObject.color = new Color(0, 0, 0, i);
            yield return null;
        }

        playerObject.ToggleControls();
    }

    public IEnumerator Final()
    {
        for (float i = 0; i < 1f; i += Time.deltaTime / 5)
        {
            fadeObject.color = new Color(0, 0, 0, i);
            yield return null;
        }

        playerObject.ToggleControls();

        foreach (BasicEye eye in eyes)
        {
            eye.OpenAgain();
        }

        float elapsedTime = 0;
        while (elapsedTime < 10)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        sphere.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        fadeObject.enabled = false;
    }
}
