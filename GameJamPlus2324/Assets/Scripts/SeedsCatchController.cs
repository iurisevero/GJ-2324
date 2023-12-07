using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsCatchController : MonoBehaviour
{
    [SerializeField] private WorldUIController worldUIController;
    private SeedController currentSeedController;

    private void OnPlayerExitSeedAreaEvent(PlayerExitSeedAreaEvent evt) => 
        OnPlayerExitSeedAreaHandler();
    private void OnGetEKeyDownEvent(GetEKeyDownEvent evt) => GetEKeyDownHandler();

    public void Awake()
    {
        EventManager.AddListener<PlayerEnterSeedAreaEvent>(OnPlayerEnterSeedAreaHandler);
        EventManager.AddListener<PlayerExitSeedAreaEvent>(OnPlayerExitSeedAreaEvent);
        EventManager.AddListener<GetEKeyDownEvent>(OnGetEKeyDownEvent);
    }

    private void OnPlayerEnterSeedAreaHandler(PlayerEnterSeedAreaEvent evt)
    {
        currentSeedController = evt.seedController;
        worldUIController.ShowPressE();
    }

    private void OnPlayerExitSeedAreaHandler()
    {
        currentSeedController = null;
        worldUIController.HidePressE();
    }

    private void GetEKeyDownHandler()
    {
        if(currentSeedController != null)
        {
            Player.Instance.UpdateSeeds(currentSeedController.earthTreeSeedType, 1);
            worldUIController.HidePressE();
            GameObjectPoolController.Enqueue(currentSeedController.GetComponent<Poolable>());
            currentSeedController = null;
        }
    }

    public void OnDestroy()
    {
        EventManager.RemoveListener<PlayerEnterSeedAreaEvent>(OnPlayerEnterSeedAreaHandler);
        EventManager.RemoveListener<PlayerExitSeedAreaEvent>(OnPlayerExitSeedAreaEvent);
        EventManager.RemoveListener<GetEKeyDownEvent>(OnGetEKeyDownEvent);
    }
}
