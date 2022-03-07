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
    public int matchTimes;
    private int match_cnt;

    // ゲームデータ
    private Deck m_deck;
    private Turn m_turn;
    private int turn_cnt;
    private int player_cnt;
    private bool turn_rev;
    [HideInInspector] string winner;

    // スプライトデータ
    [HideInInspector] public List<Sprite> cardSprites;

    // Cardオブジェクト
    DisplayCard openCard_displayCard;
    DisplayCard deckTopCard_displayCard;

    // ログ出力の有無
    public bool logEnable;

    // Updateタイマー
    private float timer;

    // Update間隔
    [SerializeField] float updateTime;

    void Start()
    {
        // 読み込みなど
        Debug.unityLogger.logEnabled = logEnable;
        cardSprites = Resources.LoadAll<Sprite>("UNO-Cards").ToList();
        openCard_displayCard = GameObject.Find("OpenCard").GetComponent<DisplayCard>();
        deckTopCard_displayCard = GameObject.Find("DeckTopCard").GetComponent<DisplayCard>();

        match_cnt = 0;
        timer = 0f;
        GameInitialize();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateTime) {
            StartCoroutine(GameProcess());
            timer = 0f;
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

    IEnumerator GameProcess()
    {
        if (winner != null)
        {
            if (match_cnt > 0 && match_cnt < matchTimes) GameInitialize();
            else if (match_cnt == -1) yield break;
            else
            {
                match_cnt = -1;
                Debug.Log("All rounds have ended!");
                yield break;
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
        openCard_displayCard.DisplayOpenCard(m_turn);
        deckTopCard_displayCard.DisplayDeckTopCard();
        

        Debug.Log($"========== TURN{turn_cnt} - {player_cnt + 1}P ==========");
        Debug.Log($"Current open card : {m_turn.m_open_card.ShowCard()}");


        Player now_player = m_players[player_cnt];

        now_player.ShowHand();
        now_player.ShowPlayableHand(open_card);
        
        // プレイヤーのプレイ
        m_turn.Action(player_cnt);

        openCard_displayCard.DisplayOpenCard(m_turn);

        foreach (Player player in m_players)
        {
            if (player.CheckWin())
            {
                winner = player.m_name;

                Debug.Log($"{winner} has won!");
                Debug.Log("This game has ended!");
            }
        }

        if (winner != null) yield break;
        if (now_player.m_played_card == null) yield break;

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
}
