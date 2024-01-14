using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static int   pipesPassed     { get; set; }
    public static float vitality        { get; set; }
    public static bool  isWkeyEnabled   { get; set; }
    public static float pipeSpawnPeriod { get; set; }
    public static float vitalityPeriod  { get; set; }
    public static bool  isPipeHitted    { get; set; }
	public static float gameTime; // For gametime
	public static bool gameRunning; // flag for game running

	public static String ToJson()
    {
		return JsonUtility.ToJson(new SavedSettings());
	}

    public static void FromJson(String json)
    {
        var settings = JsonUtility.FromJson<SavedSettings>(json);
        isWkeyEnabled   = settings.isWkeyEnabled;
        pipeSpawnPeriod = settings.pipeSpawnPeriod;
        vitalityPeriod  = settings.vitalityPeriod;
    }
}

[Serializable]
public class SavedSettings
{
	public bool  isWkeyEnabled;
	public float pipeSpawnPeriod;
	public float vitalityPeriod;
    public SavedSettings()
    {
        isWkeyEnabled   = GameState.isWkeyEnabled;
		pipeSpawnPeriod = GameState.pipeSpawnPeriod;
		vitalityPeriod  = GameState.vitalityPeriod;
    }
}
