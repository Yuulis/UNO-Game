using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    // 山札
    Deck m_deck;

    // プレイヤリスト
    List<Player> m_players;
    int players_num;

    // オープンカード(捨て山の一番上のカード)
    public Card m_open_card;

    public Turn(Deck deck, List<Player> playersList)
    {
        m_deck = deck;
        m_players = playersList;
        players_num = m_players.Count;
        m_open_card = m_deck.DrawedCard();

        Initialize();
    }

    /// <summary>
    /// 最初のターンに実行
    /// 初期オープンカードの抽選
    /// 各プレイヤーの初期手札抽選
    /// </summary>
    public void Initialize()
    {
        //TODO : 初期オープンカードの再抽選は行わず、そのカードの効果を最初のプレイヤーに対して発動させる

        while (true)
        {
            try
            {
                int open_card_valueNum = Int32.Parse(m_open_card.m_value);
                break;
            }
            catch (ArgumentNullException)
            {
                m_open_card = m_deck.DrawedCard();
            }
            catch (FormatException)
            {
                m_open_card = m_deck.DrawedCard();
            }
        }

        for (int i = 0; i < 7; i++)
        {
            foreach (Player player in m_players)
            {
                player.DrawCard(m_deck, m_open_card);
            }
        }
        
        Debug.Log("The first open card is " + m_open_card.ShowCard());
    }

    /// <summary>
    /// プレイヤーのプレイ
    /// </summary>
    /// <param name="player">現ターンでプレイ中のプレイヤー</param>
    /// <param name="opponent">それ以外のプレイヤー</param>    
    public void Action(int player_cnt)
    {
        Player now_player = m_players[player_cnt];

        now_player.EvaluateHand(m_open_card);

        // プレイ可能なカードがすでに手札にあるとき
        if (now_player.m_hand_playable.Count > 0)
        {
            now_player.RandomPlay(m_deck);
            m_open_card = now_player.m_played_card;
            now_player.EvaluateHand(m_open_card);
        }

        // ドローが必要なとき
        else
        {
            now_player.DrawCard(m_deck, m_open_card);
            now_player.EvaluateHand(m_open_card);

            // プレイ可能なカードを引いたとき
            if (now_player.m_hand_playable.Count > 0)
            {
                now_player.RandomPlay(m_deck);
                m_open_card = now_player.m_played_card;
                now_player.EvaluateHand(m_open_card);
            }

            // プレイ可能なカードを引けなかったとき
            else
            {
                now_player.m_played_card = null;
            }
        }

        foreach (Player player in m_players)
        {
            if (player.CheckWin()) return;
        }

        if (now_player.m_played_card != null)
        {
            if (now_player.m_played_card.m_value == "DT") ActionPlus(player_cnt, 2);
            if (now_player.m_played_card.m_value == "WDF") ActionPlus(player_cnt, 4);
        }
    }

    /// <summary>
    /// ドロー2やワイルドドロー4がプレイされた場合
    /// </summary>
    /// <param name="player">現ターンでプレイ中のプレイヤー(カードを出したプレイヤー)</param>
    /// <param name="opponent">それ以外のプレイヤー</param>
    /// <param name="penalty">引くカードの枚数</param>
    public void ActionPlus(int player_cnt, int penalty)
    {
        bool hit = true;
        int cnt = 1;
        while (hit)
        {
            hit = false;
            for (int i = 0; i < players_num; i++)
            {
                foreach (Card card in m_players[(player_cnt + cnt) % players_num].m_hand)
                {
                    if (card.m_value == "DT" && penalty == 2)
                    {
                        m_players[(player_cnt + cnt) % players_num].CounterPlay(m_deck, m_open_card, card);
                        hit = true;
                        cnt++;
                        break;
                    }
                    else if (card.m_value == "WDF")
                    {
                        m_players[(player_cnt + cnt) % players_num].CounterPlay(m_deck, m_open_card, card);
                        hit = true;
                        cnt++;
                        break;
                    }
                }

                if (m_players[(player_cnt + cnt) % players_num].CheckWin()) return;
                if (!hit) break;
            }
        }

        int penalty_cards = cnt * penalty;

        Debug.Log(m_players[(player_cnt + cnt) % players_num].m_name + " has to draw " + penalty_cards.ToString() + " cards");

        for (int i = 0; i < penalty_cards; i++)
        {
            m_players[(player_cnt + cnt) % players_num].DrawCard(m_deck, m_open_card);
        }
    }
}
