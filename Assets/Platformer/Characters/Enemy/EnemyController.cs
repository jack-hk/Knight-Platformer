using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class EnemyController : EntityController
{
    [SerializeField] private GameObject particles;
    SpriteRenderer entityRenderer;

    [Header("Sounds")]
    AudioSource SFX;
    private void Awake()
    {
        common = GameObject.Find("Controller").GetComponent<GameController>();

        entityRenderer = GetComponent<SpriteRenderer>();

        SFX = GetComponent<AudioSource>();
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
        ExitHurt(entityRenderer);
    }

    private void Start()
    {
        health = maxHealth;
        particles.SetActive(false);
    }

    public override void OnHit(Collision2D contact)
    {
        PlayerMovement playerMovement;
        if (contact.gameObject.CompareTag("Player"))
        {
            playerMovement = contact.gameObject.GetComponent<PlayerMovement>();
            if (!isDead && playerMovement.isAttacking)
            {
                SFX.pitch = 2f;
                SFX.Play();
                health -= 50;
                if (!isHurt)
                {
                    OnHurt(entityRenderer);
                }
            }
        }
    }

    public override void Death()
    {
        isDead = true;
        particles.SetActive(true);
        SFX.pitch = 0.5f;
        SFX.Play();
        if (common.SecondsTimer(1))
        {
            Destroy(gameObject);
        }
    }
}
