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

public class SeasonManager : Singleton<SeasonManager>
{
    [SerializeField]
    private ESeason season;

    public ESeason Season
    { get { return season; } }

    void Start()
    {
        season = ESeason.Summer;    
    }

    void Update()
    {
        
    }
}
