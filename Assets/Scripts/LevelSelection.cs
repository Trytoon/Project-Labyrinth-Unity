using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{

    public Button[] ButtonsList;

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("level", 1);
        
        for (int i = levelReached; i < ButtonsList.Length; i++)
        {
            ButtonsList[i].interactable = false;
        }
    }


    public void OnClick(Button b)
    {
        string levelID = GameObject.Find(b.name).GetComponentInChildren<Text>().text;
        int sceneID = int.Parse(levelID);
        SceneManager.LoadScene(sceneID);
        
    }

}
