using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIController : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public Image waveFill;
    public Button instaStartButton;
    public Image instaStartButtonImg;
    public TextMeshProUGUI timer;
    int totalMonsters;

    public void SetWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void SetWaveFill(int _totalMonsters)
    {
        totalMonsters = _totalMonsters;
        waveFill.fillAmount = 1;
    }

    public void UpdateWaveFill(int currentMonsters)
    {
        waveFill.fillAmount = (float)currentMonsters / totalMonsters;
    }

    public void UpdateTime(float time)
    {
        time = Mathf.Max(time, 0);
        timer.text = time.ToString("0.00") + " s";
    }

    public void SetInstaStartButton(bool active)
    {
        instaStartButton.gameObject.SetActive(active);
        instaStartButtonImg.gameObject.SetActive(active);
    }
}