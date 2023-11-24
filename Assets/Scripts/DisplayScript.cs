using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    [SerializeField]
    private Image vitalityIndicator;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        text.text = GameState.pipesPassed.ToString();
        vitalityIndicator.fillAmount = GameState.vitality;
        vitalityIndicator.color = new Color(1 - GameState.vitality, GameState.vitality, 0.3f);
    }
}
