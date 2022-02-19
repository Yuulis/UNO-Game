using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<Card> m_cards;
    List<Card> m_discarded_cards;

    public Deck()
    {
        m_cards = new List<Card>();
        m_discarded_cards = new List<Card>();

        BuildDeck();
        ShuffleDeck();
    }

    public void BuildDeck()
    {
        string[] colors = { "r", "y", "g", "b" };
        string[] actions = { "SKIP", "REV", "DT" };
        string[] specials = { "WILD", "WDF" };

        Card[] cards_zero = new Card[4];
        for (int i = 0; i < 4; i++)
        {
            cards_zero[i] = new Card(colors[i], "0");
        }

        Card[] cards_num = new Card[72];
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                for (int k = 1; k <= 9; k++)
                {
                    cards_num[i * j * k] = new Card(colors[j - 1], k.ToString());
                }
            }
        }

        Card[] cards_action = new Card[24];
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                for (int k = 1; k <= 3; k++)
                {
                    cards_action[i * j * k] = new Card(colors[j - 1], actions[k - 1]);
                }
            }
        }

        Card[] cards_special = new Card[8];
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                cards_special[i * j] = new Card("sp", specials[j - 1]);
            }
        }

        m_cards.AddRange(cards_zero);
        m_cards.AddRange(cards_num);
        m_cards.AddRange(cards_action);
        m_cards.AddRange(cards_special);
    }

    public void ShuffleDeck()
    {
        List<Card> temp = new List<Card>();
        temp = m_cards.OrderBy(i => Guid.NewGuid()).ToList();
        m_cards = temp;
    }

    public void Discard(Card card)
    {
        m_discarded_cards.Add(card);
    }

    public Card DrawCard()
    {
        if (m_cards.Count == 0)
        {
            m_cards = m_discarded_cards;
            m_discarded_cards.Clear();
        }

        Card card = m_cards[0];
        m_cards.RemoveAt(0);
        return card;
    }

    // デバッグ用
    public void ShowDeck() {
        foreach (Card card in m_cards)
        {
            card.ShowCard();
        }
    }

    public void ShowDiscardedDeck()
    {
        foreach (Card card in m_discarded_cards)
        {
            card.ShowCard();
        }
    }
}
