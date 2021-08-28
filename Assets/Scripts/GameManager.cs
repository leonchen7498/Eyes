using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public float TimeNeededToShowEye = 8f;
    public DrawEye heldEye;
    public bool particleEffect;
    public Sphere sphere;

    private List<BasicEye> eyes;
    private int AmountOfEyesSolved;

    private GameManager(){}

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
    }

    void Update()
    {
        
    }

    public void EyeSolved()
    {
        AmountOfEyesSolved++;
        foreach(BasicEye eye in eyes)
        {
            if (AmountOfEyesSolved == eye.eyesNeededToAppear)
            {
                eye.Appear();
            }
        }
    }
}
