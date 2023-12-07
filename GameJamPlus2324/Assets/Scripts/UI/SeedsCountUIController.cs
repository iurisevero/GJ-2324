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

    public void Awake()
    {
        EventManager.AddListener<UpdateInventoryEvent>(OnUpdateInventoryHandler);
    }

    public void OnUpdateInventoryHandler(UpdateInventoryEvent evt)
    {
        grapeSeedsText.text = evt.seeds[EarthTreeType.Grape].ToString();
        bananaSeedsText.text = evt.seeds[EarthTreeType.Banana].ToString();
        avocatoSeedsText.text = evt.seeds[EarthTreeType.Avocado].ToString();
        strawberrySeedsText.text = evt.seeds[EarthTreeType.Strawberry].ToString();
    }
}
