using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        if(SeedsSpawner.Instance != null)
            Destroy(SeedsSpawner.Instance.gameObject);
        
        if(Player.Instance != null)
            Destroy(Player.Instance.gameObject);
    }
}
