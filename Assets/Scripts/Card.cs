using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    string m_color;
    string m_value;

    bool EvaluateCard(string open_color, string open_value)
    {
        if (this.m_color == open_color || this.m_value == open_value || this.m_value == "ch" || this.m_value == "wdr4") return true;
        else return false;
    }
}
