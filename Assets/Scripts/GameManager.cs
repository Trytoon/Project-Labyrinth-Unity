using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static int score = 0;
    private static int restartScore = 0;
    private static int lives = 3;
    private static int restartlives = 3;

    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lives", lives);
        
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
        score = PlayerPrefs.GetInt("score");
       
        lives = PlayerPrefs.GetInt("lives", lives);
    }


    public static int GetSceneName()
    {
        
        return SceneManager.GetActiveScene().buildIndex;
    }
}
