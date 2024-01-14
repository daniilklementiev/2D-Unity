using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    [SerializeField]
    private TMPro.TextMeshProUGUI timer;
    [SerializeField]
    private Image vitalityIndicator;

    void Start()
    {
        GameState.gameRunning = true;
        GameState.gameTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    void LateUpdate()
    {
        text.text = GameState.pipesPassed.ToString();
        vitalityIndicator.fillAmount = GameState.vitality;
        vitalityIndicator.color = new Color(1 - GameState.vitality, GameState.vitality, 0.3f);
    }

	IEnumerator UpdateTimer()
	{
		while (GameState.gameRunning)
		{
			GameState.gameTime += Time.deltaTime; 
			UpdateTimerText();
			yield return null;
		}
	}

	void UpdateTimerText()
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(GameState.gameTime);
		string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		timer.text = formattedTime;
	}
}
