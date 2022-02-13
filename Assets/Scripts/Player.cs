using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string m_name;
    List<Card> m_hand;
    List<Card> m_hand_playable;
    int m_card_playable_num;

    public Player(string name)
    {
        m_name = name;
        m_hand = new List<Card>();
        m_hand_playable = new List<Card>();
        m_card_playable_num = 0;
    }

    public void EvaluateHand(Card open_card)
    {
        m_hand_playable.Clear();

        foreach (Card card in m_hand)
        {
            if (card.EvaluateCard(open_card.m_color, open_card.m_value))
            {
                m_hand_playable.Add(card);
            }
        }
    }

    public void DrawCard(Deck deck, Card open_card)
    {
        Card card = deck.DrawCard();
        m_hand.Add(card);
        EvaluateHand(open_card);

        // デバッグ用
        Debug.Log(m_name + " : draws " + card.ShowCard());
    }
}
