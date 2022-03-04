using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    // プレイヤーリスト
    List<Player> m_players = new List<Player>();

    public Game(List<string> playersName)
    {
        Debug.Log("New game has started!");

        foreach (string name in playersName)
        {
            m_players.Add(new Player(name));
        }
        Deck deck = new Deck();
        Turn turn = new Turn(deck, m_players);

        int turn_cnt = 0;
        int player_cnt = 0;
        bool turn_rev = false;
        string winner;

        Debug.Log("The first player is " + m_players[0].m_name);

        while (true)
        {
            turn_cnt++;

            if (turn_cnt != 1)
            {
                if (turn_rev) player_cnt--;
                else player_cnt++;

                if (player_cnt >= m_players.Count) player_cnt %= m_players.Count;
                else if (player_cnt < 0) player_cnt = m_players.Count + player_cnt;
            }

            Card open_card = turn.m_open_card;

            Debug.Log("========== TURN" + turn_cnt.ToString() + " - " + (player_cnt + 1).ToString() + "P ==========");
            Debug.Log("Current open card : " + turn.m_open_card.ShowCard());

            Player now_player = m_players[player_cnt];

            now_player.ShowHand();
            now_player.ShowPlayableHand(open_card);

            // プレイヤーのプレイ
            turn.Action(player_cnt);

            bool flag = false;
            foreach (Player player in m_players)
            {
                if (player.CheckWin())
                {
                    winner = player.m_name;
                    flag = true;

                    Debug.Log(winner + " has won!");

                    break;
                }
            }
            if (flag) break;

            if (now_player.m_played_card != null)
            {
                if (now_player.m_played_card.m_value == "SKIP")
                {
                    Debug.Log("The next player of " + now_player.m_name + " will be skipped!");

                    if (turn_rev) player_cnt--;
                    else player_cnt++;
                }
                else if (now_player.m_played_card.m_value == "REV")
                {
                    Debug.Log("Turn will be reversed!");

                    turn_rev = !turn_rev;
                }
            }
        }
    }

    ~Game()
    {
        Debug.Log("This game has ended!");
    }
}
