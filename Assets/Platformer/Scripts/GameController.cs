using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//JackHK
//[RequireComponent(typeof(Rigidbody2D))]
public class GameController : MonoBehaviour
{
    // --------------Data-------------- 
    public int globalScore = 0;

    private double timer = 0;

    public GameObject deathScreen;
    public Text playerHealth, playerScore;

    // --------------In-Built-------------- 

    // --------------Functions-------------- 
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*public void DeathScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    // ---------------Common----------------

    public bool SecondsTimer(double maxTime)
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
//abstract class = same as normal class but instances cannot be made
//virtualistaion

/*
class Entity
{
    protected float health;
    protected float maxHealth;

    public virtual void Damage(float damage)
    {

    }
}

class Player : Entity, IDamagable
{
    public void Damage()
    {

    }
}

class WeakEntity : Entity
{
    public override void Damage(float damage)
    {

    }
}

interface IDamagable
{
    public void Damage();
}

class Main
{
    public static void _Main()
    {
        Player p = new Player();
        p.Damage(2);
        Entity e = new Entity();
        Entity we = new WeakEntity();
        e.Damage(50);
        we.Damage(20);
    }
}
*/