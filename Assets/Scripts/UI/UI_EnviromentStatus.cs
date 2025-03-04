using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EnviromentStatus : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;
    public RectTransform clockHand;

    public void UpdateDateText(DateTime dateTime)
    {
        float hourAngle = -(float)(dateTime.Hour * 360f) / 24f -(float)(dateTime.Minute * 360f) / 1440f - 180f;
        if (clockHand != null) clockHand.localRotation = Quaternion.Euler(0, 0, hourAngle);

        dateText.text = $"{dateTime.Day:D2} - {dateTime.Month:D2} - {dateTime.Year:D4}";
        timeText.text = $"{dateTime.Hour:D2} : {dateTime.Minute:D2} : {dateTime.Second:D2}";
    }
}
