using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck
{
    // 山札
    public List<Card> m_cards;

    // 捨て山
    public List<Card> m_discarded_cards;

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
        // カード色、spはワイルドカードのみが持つ色
        string[] colors = { "r", "y", "g", "b", "sp" };

        // カード値
        string[] values = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "DT", "REV", "SKIP" };

        // sp用カード値
        string[] specials = { "WDF", "WILD" };

        int card_id = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                if (i != 4)
                {
                    // 0は各色1枚のみ
                    if (j == 0) m_cards.Add(new Card(colors[i], values[j], card_id, false));

                    // 他は4枚ずつ
                    else
                    {
                        m_cards.Add(new Card(colors[i], values[j], card_id, false));
                        m_cards.Add(new Card(colors[i], values[j], card_id, false));
                    }
                    card_id++;
                }

                // spカードに対して
                else
                {
                    if (j == 0)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            m_cards.Add(new Card(colors[i], specials[j], card_id, false));
                        }
                        card_id++;
                    }
                    else if (j == 1)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            m_cards.Add(new Card(colors[i], specials[j], card_id, false));
                        }
                        card_id++;
                    }
                    else continue;
                }
            }
        }
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
            ShuffleDeck();
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
        Debug.Log($"Deck : {s}");
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
        Debug.Log($"Discarded deck : {s}");
    }
}
