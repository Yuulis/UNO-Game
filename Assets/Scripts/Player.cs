using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    // プレイヤー名
    string m_name;

    // プレイヤーの手札
    public List<Card> m_hand;

    // プレイヤーの手札の中で、現在プレイ可能なカードのリスト
    public List<Card> m_hand_playable;

    // 現ターンでプレイしたカード
    public Card m_played_card;

    public Player(string name)
    {
        m_name = name;
        m_hand = new List<Card>();
        m_hand_playable = new List<Card>();
        m_played_card = null;
    }

    /// <summary>
    /// 現在の手札の中の、プレイ可能な手札を抽出
    /// </summary>
    /// <param name="open_card">オープンカード</param>
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

    /// <summary>
    /// 勝利判定
    /// </summary>
    /// <param name="player">判定するプレイヤー</param>
    /// <returns>
    /// 手札が0枚 -> true
    /// 手札が0枚より多い -> false
    /// </returns>
    public bool CheckWin()
    {
        if (m_hand.Count == 0) return true;
        else return false;
    }

    /// <summary>
    /// 山札からドロー
    /// </summary>
    /// <param name="deck">山札</param>
    /// <param name="open_card">オープンカード</param>
    public void DrawCard(Deck deck, Card open_card)
    {
        Card card = deck.DrawCard();
        m_hand.Add(card);
        EvaluateHand(open_card);

        // デバッグ用
        Debug.Log(m_name + " : draws " + card.ShowCard());
    }

    /// <summary>
    /// ランダムプレイ
    /// </summary>
    /// <param name="deck">山札</param>
    public void RandomPlay(Deck deck)
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        m_hand_playable = m_hand_playable.OrderBy(i => Guid.NewGuid()).ToList();
        foreach (Card card in m_hand)
        {
            if (card == m_hand_playable[m_hand_playable.Count - 1])
            {
                m_played_card = card;
                m_hand.Remove(card);
                m_hand_playable.RemoveAt(m_hand_playable.Count - 1);
                deck.Discard(card);

                // デバッグ用
                Debug.Log(m_name + " plays " + card.ShowCard());

                break;
            }
        }

        // 色指定できるカードなら、色を選択させる
        if (m_played_card.m_color == "sp" || m_played_card.m_value == "WDF")
        {
            m_played_card.m_color = ChooseColor();
        }
    }

    /// <summary>
    /// カードの色がワイルドの時に、色を選択させる
    /// </summary>
    /// <returns>
    /// 選択した色
    /// </returns>
    public string ChooseColor()
    {
        string max_color = "";
        Dictionary<string, int> colors = new Dictionary<string, int>();
        foreach (Card card in m_hand)
        {
            if (card.m_color != "sp") colors[card.m_color]++;
        }

        if (colors.Count > 0)
        {
            var maxVal = colors.Values.Max();
            var maxElem = colors.FirstOrDefault(c => c.Value == maxVal);
            max_color = maxElem.Key;
        }
        else
        {
            int r = Random.Range(0, 3 + 1);
            if (r == 0) max_color = "r";
            else if (r == 1) max_color = "y";
            else if (r == 2) max_color = "g";
            else if (r == 3) max_color = "b";
        }

        // デバッグ用
        Debug.Log(m_name + " chooses " + max_color);

        return max_color;
    }

    public void CounterPlay(Deck deck, Card open_card, Card counter_card)
    {
        foreach (Card card in m_hand)
        {
            if (card == counter_card)
            {
                m_played_card = card;
                m_hand.Remove(card);
                deck.Discard(card);
                EvaluateHand(open_card);

                // デバッグ用
                Debug.Log(m_name + " counters with " + card.ShowCard());

                break;
            }
        }
    }

    // ===== デバッグ用 =====
    /// <summary>
    /// 手札のカードを出力
    /// </summary>
    public void ShowHand()
    {
        Debug.Log(m_name + " 's hand : ");
        foreach (Card card in m_hand)
        {
            card.ShowCard();
        }
    }

    /// <summary>
    /// 手札の中で、現ターンでプレイ可能なカードを出力
    /// </summary>
    /// <param name="open_card">オープンカード</param>
    public void ShowPlayableHand(Card open_card)
    {
        Debug.Log(m_name + " 's playable hand : ");

        EvaluateHand(open_card);
        foreach (Card card in m_hand_playable)
        {
            card.ShowCard();
        }
    }
}
