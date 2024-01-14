using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Toggle keyWToggle;
    [SerializeField]
    private Slider pipePeriod;
    [SerializeField]
    private Slider vitalityPeriod;

	private bool isMenuShown;
    private const String settingsFilename = "Assets/Files/settings.json";

	void Start()
    {
        if(LoadSettings()) // if settings file exists
        {
            keyWToggle.isOn = GameState.isWkeyEnabled;
            pipePeriod.value = (6f -GameState.pipeSpawnPeriod) / (6f - 2f);
            vitalityPeriod.value = (60f - GameState.vitalityPeriod) / (60f - 20f);
        }
        else // if settings file does not exist
        {
			GameState.isWkeyEnabled = keyWToggle.isOn;
			OnPipePeriodSlider(pipePeriod.value);
			OnVitalityPeriodSlider(vitalityPeriod.value);
		}
       

        isMenuShown = content.activeInHierarchy;
        ToggleMenu(isMenuShown);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu(!isMenuShown);
        }
    }

	private void LateUpdate()
	{
		if(GameState.isPipeHitted)
        {
            ToggleMenu(true);
        }
	}

	private void ToggleMenu(bool isShown) 
    { 
        if(isShown)
        {
            Time.timeScale = 0f;
            isMenuShown = true;
			content.SetActive(isMenuShown);
		}
		else
        {
            Time.timeScale = 1f;
            isMenuShown = false;
			content.SetActive(isMenuShown);
            if (GameState.isPipeHitted)
            {
                // --life if pipe hitted
                foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe") )
                {
                    if (pipe != null)
                    {
                        GameObject.Destroy(pipe);
                    }
                }
                foreach(var food in GameObject.FindGameObjectsWithTag("Food"))
                {
					if (food != null)
                    {
						GameObject.Destroy(food);
					}
				}
                GameState.isPipeHitted = false;
                GameState.gameTime = 0f;
                GameState.gameRunning = true;
                GameState.pipesPassed = 0;
            }
		}
    }

    public void OnCloseButtonClick()
    {
        ToggleMenu(false);
    }

    public void OnControlWChanged( Boolean value )
    {
        GameState.isWkeyEnabled = value;
        SaveSettings();
    }

    public void OnPipePeriodSlider(Single value)
    {
		// value[0..1] -> time[6..2]
		GameState.pipeSpawnPeriod = 6f - value * (6f - 2f);
	    SaveSettings();
    }

    public void OnVitalityPeriodSlider(Single value)
    {
		GameState.vitalityPeriod = 60f - value * (60f - 20f);
	    SaveSettings();
    }
    
    private bool LoadSettings()
    {
        if(System.IO.File.Exists(settingsFilename))
        {
			GameState.FromJson(System.IO.File.ReadAllText(settingsFilename));
			return true;
		}
		return false;
    }

    private void SaveSettings()
    {
        System.IO.File.WriteAllText(settingsFilename, GameState.ToJson());
    }
}
