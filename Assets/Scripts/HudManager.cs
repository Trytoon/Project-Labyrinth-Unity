using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudManager : MonoBehaviour
{
    public Text ScoreLabel;
    public Text LivesLabel;
    public Text LevelLabel;

    public List<Image> LivesIcons = new List<Image>();
    public Image Icon;

    public BallMovementTest PlayerInformation;



    // Use this for initialization
    void Start()
    {
        Refresh();

        for (int i = 0; i < 3; i++)
        {
            LivesIcons.Add(Icon);
        }
    }
    // Show player stats in the HUD
    public void Refresh()
    {
        ScoreLabel.text = "Score: " + GameManager.GetScore();
        LivesLabel.text = "Lives: " + GameManager.GetLives();
        LevelLabel.text = "Level: " + GameManager.GetSceneName();

    }
}