using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int score;

    public static GameManager instance;

    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int scoreToGive)
    {
        score += scoreToGive;
        GameUI.instance.UpdateScoreText();
    }



    public void LevelEnd()
    {
        if(SceneManager.sceneCountInBuildSettings==SceneManager.GetActiveScene().buildIndex+1)
        {
            WinGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void GameOver()
    {
        GameUI.instance.SetEndScreen(false);
        Time.timeScale = 0.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        GameUI.instance.SetEndScreen(true);
        Time.timeScale = 0.0f;
    }

    

}
