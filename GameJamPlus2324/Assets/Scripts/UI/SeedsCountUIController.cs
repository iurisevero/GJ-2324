using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedsCountUIController : MonoBehaviour
{
    public TextMeshProUGUI grapeSeedsText;
    public TextMeshProUGUI bananaSeedsText;
    public TextMeshProUGUI avocatoSeedsText;
    public TextMeshProUGUI strawberrySeedsText;

    public void UpdateSeedsCount(Dictionary<EarthTreeType, int> seeds)
    {
        grapeSeedsText.text = seeds[EarthTreeType.Grape].ToString();
        bananaSeedsText.text = seeds[EarthTreeType.Banana].ToString();
        avocatoSeedsText.text = seeds[EarthTreeType.Avocado].ToString();
        strawberrySeedsText.text = seeds[EarthTreeType.Strawberry].ToString();
    }
}
