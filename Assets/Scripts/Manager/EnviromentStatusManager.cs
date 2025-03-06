using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class EnviromentStatusManager : Singleton<EnviromentStatusManager>, IDataPersistence
{
    private Season season;
    public UI_EnviromentStatus status;

    public Transform sun; 
    public Transform moon; 
    public float orbitRadius = 10f; 
    public Light2D globalLight;
    public Gradient gradient;

    public UnityEvent<ESeason> changeSeasonEvent;

    private void Start()
    {
        
    }

    void MoveSunAndMoon()
    {
        float timeOfDay = (season.DateTime.Hour * 60f + season.DateTime.Minute) / (24f * 60f);

        float angle = timeOfDay * 360f * Mathf.Deg2Rad;

        sun.position = new Vector3(Mathf.Cos(angle) * orbitRadius, Mathf.Sin(angle) * orbitRadius, 0);

        moon.position = new Vector3(-Mathf.Cos(angle) * orbitRadius, -Mathf.Sin(angle) * orbitRadius, 0);
    }

    void UpdateSunAndMoonLight()
    {
        int hour = season.DateTime.Hour;
        Light2D sunLight = sun.GetComponent<Light2D>();
        Light2D moonLight = moon.GetComponent<Light2D>();
        Light2D sunRLight = sun.GetComponentInChildren<Light2D>();
        Light2D moonRLight = moon.GetComponentInChildren<Light2D>();

        if (hour >= 6 && hour < 18)
        {
            sunLight.gameObject.SetActive(true);
            sunRLight.enabled = true;
            moonLight.gameObject.SetActive(false);
            moonRLight.enabled = false;
        }
        else
        {
            sunLight.gameObject.SetActive(false);
            sunRLight.enabled = false;
            moonLight.gameObject.SetActive(true);
            moonRLight.enabled = true;
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

    IEnumerator WaitToIncreaseDay()
    {
        do
        {
            status.UpdateDateText(season.DateTime);
            ChangeColorDay();
            MoveSunAndMoon();
            UpdateSunAndMoonLight();
            yield return new WaitForSeconds(1);
            season.IncreaseDate();
        } while (true);
    }

    public void ChangeColorDay()
    {
        float timeOfDay = (season.DateTime.Hour * 60f + season.DateTime.Minute) / (24f * 60f);

        globalLight.color = gradient.Evaluate(timeOfDay);
    }

    public void LoadData(GameData gameData)
    {
        season = gameData.SeasonData;
        StartCoroutine(WaitToIncreaseDay());
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.SetSeason(season);
    }
}
