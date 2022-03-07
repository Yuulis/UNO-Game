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

    public void DisplayDeckTopCard(Deck deck)
    {
        spriteRenderer.sprite = gameManager.cardSprites[deck.m_cards[0].m_sprite_id];
    }

    public void DisplayOpenCard(Turn turn)
    {
        spriteRenderer.sprite = gameManager.cardSprites[turn.m_open_card.m_sprite_id];
    }
}
