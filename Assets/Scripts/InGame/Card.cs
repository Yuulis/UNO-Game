using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    // カードの色(r, y, g, b, sp)
    public string m_color;

    // カードの値(0~9, SKIP, REV, DT, WILD, WDF)
    public string m_value;

    // カードの裏表(true -> 表  false -> 裏)
    public bool m_display;

    public Card(string color, string value, bool display)
    {
        m_color = color;
        m_value = value;
        m_display = display;
    }

    /// <summary>
    /// カードがプレイ可能か判定
    /// </summary>
    /// <param name="open_color">オープンカードのカードの色</param>
    /// <param name="open_value">オープンカードのカードの値</param>
    /// <returns>
    /// プレイ可能 -> true
    /// プレイ不可 -> false
    /// </returns>
    public bool EvaluateCard(string open_color, string open_value)
    {
        if (m_color == open_color || m_value == open_value || m_value == "WILD" || m_value == "WDF") return true;
        else return false;
    }

    /// <summary>
    /// カードの色と値を出力
    /// </summary>
    /// <returns>
    /// "(色) - (値)"
    /// </returns>
    public string ShowCard()
    {
        return this.m_color + "-" + this.m_value;
    }
}
