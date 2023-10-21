using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private int maxAmountOfAmmo = 10;
    [SerializeField] private int currentAmountOfAmmo = 10;

    public bool hasAmmo()
    {
        return currentAmountOfAmmo > 0;
    }
}