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

    void Start()
    {
        playerNames.Add("Bob");
        playerNames.Add("Alex");
        playerNames.Add("Mike");
        playerNames.Add("Mary");

        List<string> temp = new List<string>();
        temp = playerNames.OrderBy(i => Guid.NewGuid()).ToList();
        playerNames = temp;

        game = new Game(playerNames);
    }

    void Update()
    {
        
    }
}
