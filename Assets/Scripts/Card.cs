using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // カードの色(r, y, g, b, sp)
    public string m_color;

    // カードの値(0~9, SKIP, REV, DT, WILD, WDF)
    public string m_value;

    public Card(string color, string value)
    {
        m_color = color;
        m_value = value;
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

    // ===== デバッグ用 =====
    /// <summary>
    /// カードの色と値を出力
    /// </summary>
    /// <returns>
    /// "(色) - (値)"
    /// </returns>
    public string ShowCard()
    {
        return m_color + " - " + m_value;
    }
}
