using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    // プレイヤーオブジェクト
    Player m_player_1;
    Player m_player_2;

    // 親プレイヤー名
    string m_first_Player_name;

    public Game(string player_1_name, string player_2_name, string first_Player_name)
    {
        m_player_1 = new Player(player_1_name);
        m_player_2 = new Player(player_2_name);
        Deck deck = new Deck();
        Turn turn = new Turn(deck, m_player_1, m_player_2);

        int turn_cnt = 0;
        string winner = "0";

        while (true)
        {
            turn_cnt++;
            Card open_card = turn.m_open_card;

            // デバッグ用
            Debug.Log("========== TURN" + turn_cnt.ToString() + " ==========");
            Debug.Log("Current open card : " + turn.m_open_card.ShowCard(turn.m_open_card));

            Player now_player, other_player;
            if (m_first_Player_name == m_player_1.m_name)
            {
                if (turn_cnt % 2 == 1)
                {
                    now_player = m_player_1;
                    other_player = m_player_2;
                }
                else
                {
                    now_player = m_player_2;
                    other_player = m_player_1;
                }
            }
            else
            {
                if (turn_cnt % 2 == 1)
                {
                    now_player = m_player_2;
                    other_player = m_player_1;
                }
                else
                {
                    now_player = m_player_1;
                    other_player = m_player_2;
                }
            }

            // デバッグ用
            now_player.ShowHand();
            now_player.ShowPlayableHand(open_card);

            turn.Action(now_player, other_player);

            if (now_player.CheckWin())
            {
                winner = now_player.m_name;

                // デバッグ用
                Debug.Log(winner + " has won!");

                break;
            }
            if (other_player.CheckWin())
            {
                winner = other_player.m_name;

                // デバッグ用
                Debug.Log(winner + " has won!");

                break;
            }

            if (now_player.m_played_card != null)
            {
                if (now_player.m_played_card.m_value == "SKIP" || now_player.m_played_card.m_value == "REV")
                {
                    // デバッグ用
                    Debug.Log(now_player.m_name + " has another turn");

                    turn_cnt--;
                }
            }
        }
    }
}
