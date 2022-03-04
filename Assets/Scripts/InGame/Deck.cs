using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck
{
    // 山札
    List<Card> m_cards;

    // 捨て山
    List<Card> m_discarded_cards;

    public Deck()
    {
        m_cards = new List<Card>();
        m_discarded_cards = new List<Card>();

        BuildDeck();
        ShuffleDeck();
    }

    /// <summary>
    /// 山札初期化
    /// </summary>
    public void BuildDeck()
    {
        // カード色(赤, 黄, 緑, 青)、spはワイルドカードのみが持つ色
        string[] colors = { "r", "y", "g", "b" };

        // アクションカード(スキップ, リバース, ドロー2)
        string[] actions = { "SKIP", "REV", "DT" };

        // ワイルドカード(ワイルド, ワイルドドロー4)
        string[] specials = { "WILD", "WDF" };

        List<Card> cards_zero = new List<Card>();
        for (int i = 0; i < 4; i++)
        {
            cards_zero.Add(new Card(colors[i], "0"));
        }

        List<Card> cards_num = new List<Card>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    cards_num.Add(new Card(colors[j], k.ToString()));
                }
            }
        }

        List<Card> cards_action = new List<Card>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    cards_action.Add(new Card(colors[j], actions[k]));
                }
            }
        }

        List<Card> cards_special = new List<Card>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                cards_special.Add(new Card("sp", specials[j]));
            }
        }

        m_cards.AddRange(cards_zero);
        m_cards.AddRange(cards_num);
        m_cards.AddRange(cards_action);
        m_cards.AddRange(cards_special);
    }

    /// <summary>
    /// 山札シャッフル
    /// </summary>
    public void ShuffleDeck()
    {
        List<Card> temp = new List<Card>();
        temp = m_cards.OrderBy(i => Guid.NewGuid()).ToList();
        m_cards = temp;
    }

    /// <summary>
    /// プレイヤーの捨て札を捨て山に追加
    /// </summary>
    /// <param name="card">捨て札</param>
    public void Discard(Card card)
    {
        m_discarded_cards.Add(card);
    }

    /// <summary>
    /// 山札からドローさせる
    /// </summary>
    /// <returns>
    /// 山札から引いたカード
    /// </returns>
    public Card DrawedCard()
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

    /// <summary>
    /// 山札のカードを一括出力
    /// </summary>
    public void ShowDeck()
    {
        string s = "";
        for (int i = 0; i < m_cards.Count; i++)
        {
            s += m_cards[i].ShowCard();
            if (i + 1 != m_cards.Count) s += " ,";
        }
        Debug.Log("Deck : " + s);
    }

    /// <summary>
    /// 捨て山のカードを一括出力
    /// </summary>
    public void ShowDiscardedDeck()
    {
        string s = "";
        for (int i = 0; i < m_discarded_cards.Count; i++)
        {
            s += m_discarded_cards[i].ShowCard();
            if (i + 1 != m_discarded_cards.Count) s += " ,";
        }
        Debug.Log("Discarded deck : " + s);
    }
}
