using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // 参加プレイヤー数
    [SerializeField] private int participants;

    // プレイヤー名の候補リスト
    [SerializeField] private List<string> candidate_playerName;

    // プレイヤー名のリスト
    private List<string> m_playerNames;

    // プレイヤーリスト
    [HideInInspector] List<Player> m_players;

    // 再戦回数
    public int rematch;
    private int match_cnt;

    // ゲームデータ
    private Deck m_deck;
    private Turn m_turn;
    private int turn_cnt;
    private int player_cnt;
    private bool turn_rev;
    [HideInInspector] string winner;

    // ログ出力の有無
    public bool logEnable;

    void Start()
    {
        // 読み込みなど
        Debug.unityLogger.logEnabled = logEnable;

        match_cnt = 0;
        GameInitialize();
    }

    void Update()
    {
        if (winner != null) {
            if (match_cnt > 0 && match_cnt < rematch) GameInitialize();
            else if (match_cnt == -1) return;
            else
            {
                match_cnt = -1;
                Debug.Log("All rounds have ended!");
                return;
            }
        }
        
        turn_cnt++;

        if (turn_cnt != 1)
        {
            if (turn_rev) player_cnt--;
            else player_cnt++;

            if (player_cnt >= m_players.Count) player_cnt %= m_players.Count;
            else if (player_cnt < 0) player_cnt = m_players.Count + player_cnt;
        }

        Card open_card = m_turn.m_open_card;

        Debug.Log($"========== TURN{turn_cnt} - {player_cnt + 1}P ==========");
        Debug.Log($"Current open card : {m_turn.m_open_card.ShowCard()}");

        Player now_player = m_players[player_cnt];

        now_player.ShowHand();
        now_player.ShowPlayableHand(open_card);

        // プレイヤーのプレイ
        m_turn.Action(player_cnt);

        foreach (Player player in m_players)
        {
            if (player.CheckWin())
            {
                winner = player.m_name;

                Debug.Log($"{winner} has won!");
                Debug.Log("This game has ended!");
            }
        }

        if (winner != null) return;
        if (now_player.m_played_card == null) return;

        if (now_player.m_played_card.m_value == "SKIP")
        {
            Debug.Log($"The next player of {now_player.m_name} will be skipped!");

            if (turn_rev) player_cnt--;
            else player_cnt++;
        }
        else if (now_player.m_played_card.m_value == "REV")
        {
            Debug.Log("Turn will be reversed!");

            turn_rev = !turn_rev;
        }
    }

    /// <summary>
    /// ゲームの初期化
    /// </summary>
    private void GameInitialize()
    {
        match_cnt++;

        Debug.Log("New game has started!");
        Debug.Log($"########## Round{match_cnt} ##########");

        // プレイヤー名登録
        m_playerNames = new List<string>();
        for (int i = 0; i < participants; i++)
        {
            m_playerNames.Add(candidate_playerName[i]);
        }
        List<string> temp = new List<string>();
        temp = m_playerNames.OrderBy(i => Guid.NewGuid()).ToList();
        m_playerNames = temp;

        // プレイヤーリストの作成
        m_players = new List<Player>();
        foreach (string name in m_playerNames)
        {
            m_players.Add(new Player(name));
        }

        // データ初期化
        m_deck = new Deck();
        m_turn = new Turn(m_deck, m_players);
        turn_cnt = 0;
        player_cnt = 0;
        turn_rev = false;
        winner = null;

        Debug.Log($"The first player is {m_players[0].m_name}");
    }
}
