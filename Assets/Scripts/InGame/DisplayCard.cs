using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    GameManager gameManager;
    GameObject cards;
    SpriteRenderer spriteRenderer;
    List<GameObject> cardPrefabs;

    void Start()
    {
        cards = (GameObject)Resources.Load("Prefabs/Cards");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = cards.GetComponent<SpriteRenderer>();
        Initialize();
    }

    public void Initialize()
    {
        cardPrefabs = new List<GameObject>();
    }

    public void DisplayDeckTopCard()
    {
        spriteRenderer.sprite = gameManager.cardSprites[gameManager.cardSprites.Count - 1];
        GameObject obj = GameObject.Instantiate(cards, new Vector3(-15.0f, 6.0f, 0f), Quaternion.identity);

        if (cardPrefabs.Count <= 0)
        {
            cardPrefabs.Add(obj);
        }
        else
        {
            Destroy(cardPrefabs[0]);
            cardPrefabs[0] = obj;
        }

    }

    public void DisplayOpenCard(Turn turn)
    {
        spriteRenderer.sprite = gameManager.cardSprites[turn.m_open_card.m_sprite_id];
        GameObject obj = GameObject.Instantiate(cards, new Vector3(0f, 0f, 0f), Quaternion.identity);

        if (cardPrefabs.Count <= 1)
        {
            cardPrefabs.Add(obj);
        }
        else
        {
            Destroy(cardPrefabs[1]);
            cardPrefabs[1] = obj;
        }       
    }

    /*
    public void DisplayHand(List<Card> hand, int player_num)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand_spriteRenderer.sprite = gameManager.cardSprites[hand[i].m_sprite_id];

            if (player_num == 1) {
                Instantiate
                (
                    cards,
                    new Vector3((float)(0f + i), -5f, 0f),
                    Quaternion.identity
                );
            }
            
        }
    }
    */
}
