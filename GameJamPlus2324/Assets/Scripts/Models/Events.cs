using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static PlayerEnterPlantationAreaEvent PlayerEnterPlantationAreaEvent = 
        new PlayerEnterPlantationAreaEvent();
    public static PlayerExitPlantationAreaEvent PlayerExitPlantationAreaEvent = 
        new PlayerExitPlantationAreaEvent();
    public static UpdateInventoryEvent UpdateInventoryEvent = new UpdateInventoryEvent();
    public static GetEKeyDownEvent GetEKeyDownEvent = new GetEKeyDownEvent(); 
    // public static ObjectiveUpdateEvent ObjectiveUpdateEvent = new ObjectiveUpdateEvent();
    // public static AllObjectivesCompletedEvent AllObjectivesCompletedEvent = new AllObjectivesCompletedEvent();
    // public static GameOverEvent GameOverEvent = new GameOverEvent();
    // public static PlayerDeathEvent PlayerDeathEvent = new PlayerDeathEvent();
    // public static EnemyKillEvent EnemyKillEvent = new EnemyKillEvent();
    // public static PickupEvent PickupEvent = new PickupEvent();
    // public static AmmoPickupEvent AmmoPickupEvent = new AmmoPickupEvent();
    // public static DamageEvent DamageEvent = new DamageEvent();
    // public static DisplayMessageEvent DisplayMessageEvent = new DisplayMessageEvent();
}

public class PlayerEnterPlantationAreaEvent : GameEvent
{
    public TreeSpawnerAreaController treeSpawnerAreaController;
}

public class PlayerExitPlantationAreaEvent : GameEvent
{
}

public class UpdateInventoryEvent : GameEvent
{
    public Dictionary<EarthTreeType, int> seeds;
}

public class GetEKeyDownEvent : GameEvent
{
}

// public class ObjectiveUpdateEvent : GameEvent
// {
//     // public Objective Objective;
//     public string DescriptionText;
//     public string CounterText;
//     public bool IsComplete;
//     public string NotificationText;
// }

// public class AllObjectivesCompletedEvent : GameEvent { }

// public class GameOverEvent : GameEvent
// {
//     public bool Win;
// }

// public class PlayerDeathEvent : GameEvent { }

// public class EnemyKillEvent : GameEvent
// {
//     public GameObject Enemy;
//     public int RemainingEnemyCount;
// }

// public class PickupEvent : GameEvent
// {
//     public GameObject Pickup;
// }

// public class AmmoPickupEvent : GameEvent
// {
//     // public WeaponController Weapon;
// }

// public class DamageEvent : GameEvent
// {
//     public GameObject Sender;
//     public float DamageValue;
// }

// public class DisplayMessageEvent : GameEvent
// {
//     public string Message;
//     public float DelayBeforeDisplay;
// }
