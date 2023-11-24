using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    private bool isMenuShown;
    [SerializeField]
    private Toggle keyWToggle;
    [SerializeField]
    private Slider pipePeriod;
    [SerializeField]
    private Slider vitalityPeriod;

    void Start()
    {
        GameState.isWkeyEnabled = keyWToggle.isOn;
        OnPipePeriodSlider(pipePeriod.value);
        OnVitalityPeriodSlider(vitalityPeriod.value);
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
		}
    }

    public void OnCloseButtonClick()
    {
        ToggleMenu(false);
    }

    public void OnControlWChanged( Boolean value )
    {
        GameState.isWkeyEnabled = value;
    }

    public void OnPipePeriodSlider(Single value)
    {
		// value[0..1] -> time[6..2]
		GameState.pipeSpawnPeriod = 6f - value * (6f - 2f);
	}

    public void OnVitalityPeriodSlider(Single value)
    {
		GameState.vitalityPeriod = 60f - value * (60f - 20f);
	}
    
}
