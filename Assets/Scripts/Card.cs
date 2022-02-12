using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    string m_color;
    string m_value;

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
    public void ShowCard(Card card)
    {
        Debug.Log("Color : " + card.m_color + ", Value : " + card.m_value);
    }
}
