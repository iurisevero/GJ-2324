using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int maxAmountOfAmmo = 10;
    [SerializeField] private int currentAmountOfAmmo = 10;
    [SerializeField] private float timeToShoot = 1f;

    public bool hasAmmo()
    {
        return currentAmountOfAmmo > 0;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
}

public enum WeaponType
{
    Grape,
    Avocado,
    Strawberry,
    Banana
}