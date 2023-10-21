using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantationUIController : MonoBehaviour
{
    const string HideKey = "Hide";
    const string ShowKey = "Show";
    public Panel panel;
    public Button avocadoButton;
    public Button bananaButton;
    public Button grapeButton;
    public Button strawberryButton;
    public TextMeshProUGUI avocadoText;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI grapeText;
    public TextMeshProUGUI strawberryText;

    public GameObject reloadObj;
    public Image reloadFill;
    public IEnumerator fillCoroutine;

    private void Start()
    {
        reloadObj.SetActive(false);
    }

    private void TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 0.5f;
        t.equation = EasingEquations.EaseOutQuad;
        t.timeType = EasingControl.TimeType.Real;
    }

    public void Populate(Dictionary<EarthTreeType, int> seeds)
    {
        foreach(var seed in seeds)
        {
            switch (seed.Key)
            {
                case EarthTreeType.Avocado:
                    avocadoButton.interactable = seed.Value > 0;
                    avocadoText.text = seed.Value.ToString();
                    break;
                case EarthTreeType.Banana:
                    bananaButton.interactable = seed.Value > 0;
                    bananaText.text = seed.Value.ToString();
                    break;
                case EarthTreeType.Grape:
                    grapeButton.interactable = seed.Value > 0;
                    grapeText.text = seed.Value.ToString();
                    break;
                default:
                    strawberryButton.interactable = seed.Value > 0;
                    strawberryText.text = seed.Value.ToString();
                    break;
            }
        }
    }

    public void ShowReload(float reloadTime)
    {
        reloadObj.SetActive(true);
        fillCoroutine = FillReloadImage(reloadTime);
        reloadFill.fillAmount = 0;
        StartCoroutine(fillCoroutine);
    }

    public void HideReload()
    {
        Debug.Log("HideReload");
        reloadObj.SetActive(false);
        StopCoroutine(fillCoroutine);
    }


    private IEnumerator FillReloadImage(float reloadTime)
    {
        while(reloadFill.fillAmount <= 1)
        {
            reloadFill.fillAmount += 1.0f / reloadTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ShowPlantButtons()
    {
        TogglePos(ShowKey);
    }

    public void HidePlantButtons()
    {
        TogglePos(HideKey);
    }
}
