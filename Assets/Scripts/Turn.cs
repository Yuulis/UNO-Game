using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    // 
    Deck m_deck;

    // プレイヤー(1~2)
    Player m_player_1;
    Player m_player_2;

    // オープンカード(捨て札リストの一番上のカード)
    Card m_open_card;

    public Turn(Deck deck, Player player_1, Player player_2)
    {
        // TODO : 最大4人プレイに対応

        m_deck = deck;
        m_player_1 = player_1;
        m_player_2 = player_2;
        m_open_card = m_deck.DrawCard();

        Initialize();
    }

    /// <summary>
    /// 最初のターンに実行
    /// 初期オープンカードの抽選
    /// </summary>
    public void Initialize()
    {
        //TODO : 初期オープンカードの再抽選は行わず、そのカードの効果を最初のプレイヤーに対して発動させる

        int open_card_valueNum = int.Parse(m_open_card.m_value);
        while (!(0 <= open_card_valueNum && open_card_valueNum <= 9))
        {
            // デバッグ用
            Debug.Log("The first open card has to be normal.");

            m_open_card = m_deck.DrawCard();
        }

        // デバッグ用
        Debug.Log("The first open card is " + m_open_card.ShowCard());
    }

    /// <summary>
    /// プレイヤーのプレイ
    /// </summary>
    /// <param name="player">プレイ中のプレイヤー</param>
    /// <param name="opponent">それ以外のプレイヤー</param>    
    public void Action(Player player, Player opponent)
    {
        Player now_player = player;
        Player other_player = opponent;

        now_player.EvaluateHand(m_open_card);

        // プレイ可能なカードがすでに手札にあるとき
        if (now_player.m_hand_playable.Count > 0)
        {
            now_player.RandomPlay(m_deck);

            m_open_card = now_player.m_played_card;
            now_player.EvaluateHand(m_open_card);
        }
    }
}
