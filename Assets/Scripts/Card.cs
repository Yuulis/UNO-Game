using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string m_color;
    public string m_value;

    public Card(string color, string value)
    {
        m_color = color;
        m_value = value;
    }

    public bool EvaluateCard(string open_color, string open_value)
    {
        if (m_color == open_color || m_value == open_value || m_value == "WILD" || m_value == "WDF") return true;
        else return false;
    }

    // デバッグ用
    public string ShowCard()
    {
        return m_color + " - " + m_value;
    }
}
