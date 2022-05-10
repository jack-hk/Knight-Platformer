using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class CollectableController : MonoBehaviour
{
    public int heal = 20;
    public int score = 100;
    public bool isGolden = false;
    public bool isNextLevel = false;

    void OnCollisionEnter2D(Collision2D contact)
    {
        PlayerController playerController;
        PlayerMovement playerMovement;
        if (contact.gameObject.CompareTag("Player"))
        {
            playerController = contact.gameObject.GetComponent<PlayerController>();
            playerController.health += heal;
            playerController.globalScore += score;
            if (isGolden)
            {
                playerMovement = contact.gameObject.GetComponent<PlayerMovement>();
                playerMovement.isPoweredUp = true;
            }
            if (!isNextLevel)
            {
                Destroy(gameObject);
            }
        }
        if (isNextLevel)
        {
            GameController controller;
            controller = GameObject.Find("Controller").GetComponent<GameController>();
            controller.NextLevel();
        }
    }
}
