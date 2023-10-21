using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private EarthTreeType weaponType;
    [SerializeField] private int maxAmountOfAmmo = 10;
    [SerializeField] private int amountOfAmmoToRemoveInEachShot = 1;
    [SerializeField] private float cadence = 1f;


    public EarthTreeType GetWeaponType()
    {
        return weaponType;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    public float GetCadence()
    {
        return cadence;
    }

    public int GetAmountOfAmmoToRemoveInEachShot()
    {
        return amountOfAmmoToRemoveInEachShot;
    }

    public int GetMaxAmountOfAmmo()
    {
        return maxAmountOfAmmo;
    }
}