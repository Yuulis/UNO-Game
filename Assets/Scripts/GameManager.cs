using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ゲームオブジェクト
    Game game;

    // プレイヤー名(1~2)
    string Player1_name = "Bob";
    string Player2_name = "Alex";

    //* 4プレイヤー対戦用
    // プレイヤー名のリスト
    List<string> PlayerNameList = new List<string>();

    void Start()
    {
        game = new Game(Player1_name, Player2_name, Player1_name);
    }

    void Update()
    {
        
    }
}
