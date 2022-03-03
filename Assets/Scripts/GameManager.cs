using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ゲームオブジェクト
    Game game;

    // プレイヤー名のリスト
    List<string> playerNames = new List<string>();

    // プレイヤー名の候補リスト
    public List<string> candidate_playerName;

    // 参加プレイヤー数
    public int participants;

    void Start()
    {
        for (int i = 0; i < participants; i++)
        {
            playerNames.Add(candidate_playerName[i]);
        }

        List<string> temp = new List<string>();
        temp = playerNames.OrderBy(i => Guid.NewGuid()).ToList();
        playerNames = temp;

        game = new Game(playerNames);
    }

    void Update()
    {
        
    }
}
