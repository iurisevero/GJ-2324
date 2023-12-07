using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public static bool paused;
    public Dictionary<EarthTreeType, int> seeds { private set; get; }
    [SerializeField] ShootManager shootManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        seeds = new Dictionary<EarthTreeType, int>()
        {
            { EarthTreeType.Avocado, 0 },
            { EarthTreeType.Banana, 0 },
            { EarthTreeType.Grape, 0 },
            { EarthTreeType.Strawberry, 0 },
        };
        UpdateSeeds( EarthTreeType.Avocado, 0);
    }

    public void UpdateSeeds(EarthTreeType earthTreeType, int amount)
    {
        seeds[earthTreeType] += amount;
        UpdateInventoryEvent updateInventoryEvent = Events.UpdateInventoryEvent;
        updateInventoryEvent.seeds = seeds;
        EventManager.Broadcast(updateInventoryEvent);
    }

    public WeaponSO GetCurrentWeapon()
    {
        return shootManager._selectedWeaponType;
    }

    public void Reload(EarthTreeType earthTreeType)
    {
        shootManager.FullfillAmmo(earthTreeType);
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    public static void UnPause()
    {
        Time.timeScale = 1f;
        paused = false;
    }
}