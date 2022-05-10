using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class EntityController : MonoBehaviour
{
    // --------------Data-------------- 
    protected GameController common;

    public int health;
    public int globalScore;
    protected bool isDead = false;
    protected bool isHurt = false;

    [Header("Configuration")]
    public int maxHealth = 100;

    // --------------In-Built-------------- 

    private void Awake()
    {
        common = GameObject.Find("Controller").GetComponent<GameController>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (gameObject != null)
        {
            if (health <= 0)
            {
                Death();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D contact)
    {
        OnHit(contact);
    }

    // --------------Functions--------------

    protected void OnHurt(SpriteRenderer sprite)
    {
        isHurt = true;
        sprite.color = Color.red;

    }
    protected void ExitHurt(SpriteRenderer sprite)
    {
        if (common.SecondsTimer(1) && isHurt)
        {
            sprite.color = Color.white;
            isHurt = false;
        }
    }

    public virtual void OnHit(Collision2D contact) { }

    public virtual void Death() { }
}
