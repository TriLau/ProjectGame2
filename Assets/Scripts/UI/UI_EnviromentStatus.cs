using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EnviromentStatus : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    public void UpdateDateText(DateTime dateTime)
    {
        dateText.text = $"day: {dateTime.Day}, mouth: {dateTime.Month}, year: {dateTime.Year}";
    }
}
