using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//JackHK

public class GameController : MonoBehaviour
{

    // --------------Functions-------------- 
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
