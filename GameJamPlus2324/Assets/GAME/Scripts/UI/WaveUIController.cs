using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIController : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public Image waveFill;
    int totalMonsters;
    
    public void SetWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void SetWaveFill(int _totalMonsters)
    {
        totalMonsters = _totalMonsters;
    }

    public void UpdateWaveFill(int currentMonsters)
    {
        waveFill.fillAmount = (float)currentMonsters / totalMonsters;
    }
}
