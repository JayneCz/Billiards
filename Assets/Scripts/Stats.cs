using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Stats
{
    private const string PLAYER_PREFS_KEY = "stats";

    public float time = 0f;
    public int shots = 0;
    public int points = 0;
    public static Stats Load()
    {
        return JsonUtility.FromJson<Stats>(PlayerPrefs.GetString(PLAYER_PREFS_KEY));
    }
    public static void Save(Stats stats)
    {
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, JsonUtility.ToJson(stats));
    }
    public void Save()
    {
        Stats.Save(this);
    }
    public override string ToString()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return $"Time: {timeSpan:mm\\:ss}\nShots: {shots}\nPoints: {points}";
    }


}
