using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // プレイヤー1~2の名前
    string m_player_1_name;
    string m_player_2_name;

    // 親プレイヤー名
    string m_firstPlayer_name;

    Game(string player_1_name, string player_2_name, string firstPlayer_name)
    {
        Player player_1 = new Player(m_player_1_name);
        Player player_2 = new Player(m_player_2_name);
        Deck deck = new Deck();
        Turn turn = new Turn(deck, player_1, player_2);

        int turn_cnt = 0;
        string winner = "0";

        while (winner == "0")
        {
            turn_cnt++;
            Card open_card = turn.m_open_card;

            // デバッグ用
            Debug.Log("========== TURN" + turn_cnt.ToString() + " ==========");
            Debug.Log("Current open card : " + turn.m_open_card.ShowCard());

            Player now_player;
            Player other_player;
            if (m_firstPlayer_name == player_1_name)
            {
                if (turn_cnt % 2 == 1)
                {
                    now_player = player_1;
                    other_player = player_2;
                }
                else
                {
                    now_player = player_2;
                    other_player = player_1;
                }
            }
            else
            {
                if (turn_cnt % 2 == 1)
                {
                    now_player = player_2;
                    other_player = player_1;
                }
                else
                {
                    now_player = player_1;
                    other_player = player_2;
                }
            }

            // デバッグ用
            now_player.ShowHand();
            now_player.ShowPlayableHand(open_card);

            turn.Action(now_player, other_player);

            if (now_player.CheckWin())
            {
                winner = now_player.name;

                // デバッグ用
                Debug.Log(winner + " has won!");

                break;
            }
            if (other_player.CheckWin())
            {
                winner = other_player.name;

                // デバッグ用
                Debug.Log(winner + " has won!");

                break;
            }

            if (now_player.m_played_card.m_value == "SKIP" || now_player.m_played_card.m_value == "REV")
            {
                // デバッグ用
                Debug.Log(now_player.name + " has another turn");

                turn_cnt--;
            }

            if (turn_cnt > 0 && turn_cnt % 2 == 0)
            {
                // デバッグ用
                Debug.Log(now_player.name + "'s turn again");

                turn_cnt--;
            }
        }

        
    }
}
