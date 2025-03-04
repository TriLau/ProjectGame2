using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnviromentStatusManager : Singleton<EnviromentStatusManager>, IDataPersistence
{
    private Season season;
    public UI_EnviromentStatus status;
    private UnityEvent<ESeason> changeSeasonEvent;

    private void Update()
    {
        if (true)
        {
            StartCoroutine(WaitToAddDay(4));
            season.AddDate();
            status.UpdateDateText(season.DateTime);
        }
    }

    public bool ChangeSeason()
    {
        switch (season.DateTime.Month)
        {
            case 1:
            case 2:
            case 3:
                {
                    season.SetSeasonStatus(ESeason.Spring);
                    return true;
                }
            case 4:
            case 5:
            case 6:
                {
                    season.SetSeasonStatus(ESeason.Summer);
                    return true;
                }
            case 7:
            case 8:
            case 9:
                {
                    season.SetSeasonStatus(ESeason.Autumn);
                    return true;
                }
            case 10:
            case 11:
            case 12:
                {
                    season.SetSeasonStatus(ESeason.Winter);
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    public ESeason GetCurrentSeason()
    {
        return season.SeasonStatus;
    }

    IEnumerator WaitToAddDay(int i)
    {
        while (i > 0)
        {
            yield return new WaitForSeconds(1);
            i--;
        }
    }

    public void LoadData(GameData gameData)
    {
        season = gameData.Season;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.SetSeason(season);
    }
}
