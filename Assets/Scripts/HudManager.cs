using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HudManager : MonoBehaviour
{
    public Text ScoreLabel;
    public Text LivesLabel;
    public Text LevelLabel;
    public Text TimerLabel;

    public BallMovementTest PlayerInformation;

    float currentTime = 0.0f;

    public bool gameIsPaused = false;

    public GameObject InfoUI;
    public GameObject PauseMenuUI;

    public GameObject SliderUI;
    public Slider slider;
    
    public float fillSpeed;

    // Use this for initialization
    void Start()
    {

        Refresh();
    }
    // Show player stats in the HUD
    public void Refresh()
    {


        if (InfoUI.activeSelf)
        {

            ScoreLabel.text = "Score: " + GameManager.GetScore();
            LivesLabel.text = "Lives: " + GameManager.GetLives();
            LevelLabel.text = "Level: " + GameManager.GetSceneName();

            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            TimerLabel.text = "Time: " + time.ToString(@"mm\:ss");
        }
    }


    public void Update()
    {
        Refresh();

        if (InfoUI.activeSelf)
        {
            currentTime += Time.deltaTime;
        }
        
        if (SliderUI.activeSelf)
        {
            if (slider.value < 1.0f)
            {
                slider.value += fillSpeed * Time.deltaTime;
                print(slider.value);
            }
            else
            {
                SliderUI.SetActive(false);
            }

            
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        InfoUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        InfoUI.SetActive(true);
        PauseMenuUI.SetActive(false);

    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.resetStats();
        Refresh();
    }

    public void DisplayMainMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.setScore(0);
        GameManager.setLives(3);
        SceneManager.LoadScene(0); 
        
    }

    public void DisplayLoadBar(float speed)
    {
        fillSpeed = speed;
        slider.value = 0.0f;
        SliderUI.SetActive(true);
    }
}