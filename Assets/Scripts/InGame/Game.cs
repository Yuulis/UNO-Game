using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    // プレイヤーリスト
    List<Player> m_players;

    public Game(List<string> playersName)
    {
        m_players = new List<Player>();
        foreach (string name in playersName)
        {
            m_players.Add(new Player(name));
        }
        Deck deck = new Deck();
        Turn turn = new Turn(deck, m_players);

        int turn_cnt = 0;
        string winner;
        while (true)
        {
            turn_cnt++;
            Card open_card = turn.m_open_card;

            // ログ出力
            Debug.Log("========== TURN" + turn_cnt.ToString() + " ==========");
            Debug.Log("Current open card : " + turn.m_open_card.ShowCard(turn.m_open_card));

            Player now_player = m_players[turn_cnt % 4];

            // ログ出力
            now_player.ShowHand();
            now_player.ShowPlayableHand(open_card);

            // プレイヤーのプレイ
            turn.Action(turn_cnt);

            bool flag = false;
            foreach (Player player in m_players)
            {
                if (player.CheckWin())
                {
                    winner = player.m_name;
                    flag = true;

                    // ログ出力
                    Debug.Log(winner + " has won!");

                    break;
                }
            }
            if (flag) break;

            if (now_player.m_played_card != null)
            {
                if (now_player.m_played_card.m_value == "SKIP" || now_player.m_played_card.m_value == "REV")
                {
                    // ログ出力
                    Debug.Log(now_player.m_name + " has another turn");

                    turn_cnt--;
                }
            }
        }
    }
}
