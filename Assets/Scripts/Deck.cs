using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // 山札
    List<Card> m_cards;

    // 捨て札リスト
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
        // カード色(赤, 黄, 緑, 青) spはワイルドカードのみが持つ色
        string[] colors = { "r", "y", "g", "b" };

        // アクションカード(スキップ, リバース, ドロー2)
        string[] actions = { "SKIP", "REV", "DT" };

        // ワイルドカード(ワイルド, ワイルドドロー4)
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
    /// プレイヤーの捨て札を捨て札リストに追加
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

    // ===== デバッグ用 =====
    /// <summary>
    /// 山札のカードを一括出力
    /// </summary>
    public void ShowDeck() {
        foreach (Card card in m_cards)
        {
            card.ShowCard();
        }
    }

    /// <summary>
    /// 捨て札リストのカードを一括出力
    /// </summary>
    public void ShowDiscardedDeck()
    {
        foreach (Card card in m_discarded_cards)
        {
            card.ShowCard();
        }
    }
}
