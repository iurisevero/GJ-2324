using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private EarthTreeType weaponType;
    [SerializeField] private int maxAmountOfAmmo = 10;
    [SerializeField] private int currentAmountOfAmmo = 10;
    [SerializeField] private int amountOfAmmoToRemoveInEachShot = 1;

    [SerializeField] private float cadence = 1f;

    public bool HasAmmo()
    {
        return currentAmountOfAmmo > 0;
    }

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

    public void RemoveAmmo()
    {
        currentAmountOfAmmo -= amountOfAmmoToRemoveInEachShot;
        currentAmountOfAmmo = Mathf.Max(0, currentAmountOfAmmo);
    }

    public void FullfillAmmo()
    {
        currentAmountOfAmmo = maxAmountOfAmmo;
    }
}