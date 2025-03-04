using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESeason
{
    Spring,
    Summer,
    Autumn,
    Winter
}

[System.Serializable]
public class Season
{
    [SerializeField] private DateTime _dateTime;
    [SerializeField] private ESeason _seasonStatus;

    public DateTime DateTime
    { get { return _dateTime; } }

    public ESeason SeasonStatus
    { get { return _seasonStatus; } }

    public Season()
    {
        _dateTime = new DateTime(1999, 1, 1, 13, 30, 00);
        _seasonStatus = ESeason.Spring;
    }

    public void SetSeasonStatus(ESeason eSeason)
    {
        _seasonStatus = eSeason;
    }

    public void SetDateTime(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public void AddDate()
    {
        _dateTime.AddDays(1);
    }
}
