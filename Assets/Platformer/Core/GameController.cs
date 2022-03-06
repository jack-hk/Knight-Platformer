using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//JackHK

public class GameController : MonoBehaviour
{
    private float timer = 0;

    // --------------Functions-------------- 
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ---------------Common----------------

    public bool SecondsTimer(float maxTime)
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            timer -= maxTime;
            return true;
        }
        return false;
    }

}

