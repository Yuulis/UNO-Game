using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ゲームスクリプト
    Game src_Game;

    // プレイヤー名のリスト
    List<string> playerNames = new List<string>();

    // プレイヤー名の候補リスト
    public List<string> candidate_playerName;

    // 参加プレイヤー数
    public int participants;

    // 再戦回数
    public int rematch;

    // ログ出力の有無
    public bool logEnable;

    void Start()
    {
        Debug.unityLogger.logEnabled = logEnable;
        src_Game = GameObject.Find("GameSystem").GetComponent<Game>();

        for (int i = 0; i < participants; i++)
        {
            playerNames.Add(candidate_playerName[i]);
        }

        List<string> temp = new List<string>();
        temp = playerNames.OrderBy(i => Guid.NewGuid()).ToList();
        playerNames = temp;

        src_Game.GameSystem(playerNames);
    }

    void Update()
    {

    }
}
