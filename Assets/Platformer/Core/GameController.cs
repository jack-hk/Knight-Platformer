using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void LoadLevel(int level) //loads the specified level
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel() //loads the next chronological level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
