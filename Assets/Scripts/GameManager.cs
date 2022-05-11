using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static int score = 0;
    private static int lives = 3;

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void IncrementScore()
    {
        score++;
    }

    public static void SetLives(int nLives)
    {
        lives = nLives;
    }


    public static int GetLives()
    {
        return lives;
    }

    public static int GetScore()
    {
        return score;
    }


    public static char GetSceneName()
    {
        string name = SceneManager.GetActiveScene().name;
        return name[name.Length-1];
    }
}
