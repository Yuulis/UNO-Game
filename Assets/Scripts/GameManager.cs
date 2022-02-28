using System.Collections;
using System.Collections.Generic;
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

        int x = Random.Range(0, 3 + 1);
        game = new Game(playerNames, playerNames[0], playerNames[1], playerNames[x]);
    }

    void Update()
    {
        
    }
}
