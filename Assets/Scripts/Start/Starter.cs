using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (PlayerPrefs.HasKey("Current Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Current Level"));
        }
        else
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}
