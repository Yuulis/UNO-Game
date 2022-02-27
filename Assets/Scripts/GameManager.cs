using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ゲームオブジェクト
    Game game;

    //* 4プレイヤー対戦用
    // プレイヤー名のリスト
    List<string> playerNames = new List<string>();

    void Start()
    {
        playerNames.Add("Bob");
        playerNames.Add("Alex");
        game = new Game(playerNames[0], playerNames[1], playerNames[0]);
    }

    void Update()
    {
        
    }
}
