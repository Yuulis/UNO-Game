using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DisplayDeckTopCard()
    {
        spriteRenderer.sprite = gameManager.cardSprites[58];
    }

    public void DisplayOpenCard(Turn turn)
    {
        spriteRenderer.sprite = gameManager.cardSprites[turn.m_open_card.m_sprite_id];
    }
}
