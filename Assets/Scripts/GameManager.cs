using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static int score = 0;
    private static int restartScore = 0;
    private static int lives = 3;
    private static int restartlives = 0;

    void Start()
    {
        score = 0;
        restartScore = score;
        restartlives = lives;
    }
    
    public static void IncrementScore(int val)
    {
        score += val;
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
    
    public static void setScore(int scoreNew)
    {
        score = scoreNew;
    }
    
    public static void setLives(int livesNew)
    {
        lives = livesNew;
    }
    
    public static void resetStats()
    {
        score = restartScore;
        lives = restartlives;
    }


    public static char GetSceneName()
    {
        string name = SceneManager.GetActiveScene().name;
        return name[name.Length-1];
    }
}
