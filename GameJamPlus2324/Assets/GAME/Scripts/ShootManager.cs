using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [Header("Weapons")] [SerializeField] List<WeaponSO> weaponSos = new List<WeaponSO>();
    [SerializeField] private List<int> weaponCurrentAmountOfAmmo = new List<int>();
    public WeaponSO _selectedWeaponType;

    private bool canShot = true;
    const string GrapeBulletPoolKey = "GrapeBullet";
    const string AvocadoBulletPoolKey = "AvocadoBullet";
    const string StrawberryBulletPoolKey = "StrawberryBullet";
    const string BananaBulletPoolKey = "BananaBullet";

    [SerializeField] private Transform bulletSpawnPosition;

    private float timeFromLastShot = 0f;

    [SerializeField] private WeaponSelectionBarUI _weaponSelectionBarUI;

    private void Awake()
    {
        _selectedWeaponType = weaponSos[0];
    }

    private void Start()
    {
        GameObjectPoolController.AddEntry(GrapeBulletPoolKey, weaponSos[0].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(AvocadoBulletPoolKey, weaponSos[1].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(StrawberryBulletPoolKey, weaponSos[2].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(BananaBulletPoolKey, weaponSos[3].GetBulletPrefab(), 3, 15);

        foreach (var weapon in weaponSos)
        {
            weaponCurrentAmountOfAmmo.Add(weapon.GetMaxAmountOfAmmo());
        }

        for (int i = 0; i < weaponSos.Count; i++)
        {
            _weaponSelectionBarUI.SetAmmoText(_weaponSelectionBarUI.GetWeaponIndex(weaponSos[i].GetWeaponType()),
                weaponCurrentAmountOfAmmo[i], weaponSos[i].GetMaxAmountOfAmmo());
        }

        // _weaponSelectionBarUI.SetAmmoText(_weaponSelectionBarUI.GetWeaponIndex(_selectedWeaponType.GetWeaponType()),
        //     weaponCurrentAmountOfAmmo[GetWeaponIndex()], _selectedWeaponType.GetMaxAmountOfAmmo());
    }

    void Update()
    {
        if (Player.paused) return;

        if (!canShot)
        {
            timeFromLastShot += Time.deltaTime;
            if (timeFromLastShot > _selectedWeaponType.GetCadence())
            {
                canShot = true;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(_selectedWeaponType);
        }
    }

    public void SwapWeapon(int swapedWeapon)
    {
        _selectedWeaponType = weaponSos[swapedWeapon];
        _weaponSelectionBarUI.SelectTheWeapon(_selectedWeaponType.GetWeaponType());
        _weaponSelectionBarUI.SelectTheWeapon(_selectedWeaponType.GetWeaponType());
    }

    void Shoot(WeaponSO weaponS0)
    {
        if (!HasAmmo()) return;

        AudioManager.Instance.Play(weaponS0.bulletSFX);
        
        EarthTreeType weaponType = weaponS0.GetWeaponType();
        switch (weaponType)
        {
            case EarthTreeType.Grape:
                Shoot(GrapeBulletPoolKey);
                break;
            case EarthTreeType.Avocado:
                Shoot(AvocadoBulletPoolKey);
                break;
            case EarthTreeType.Strawberry:
                Shoot(StrawberryBulletPoolKey);
                break;
            case EarthTreeType.Banana:
                Shoot(BananaBulletPoolKey);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
        }

        _weaponSelectionBarUI.SetAmmoText(_weaponSelectionBarUI.GetWeaponIndex(weaponS0.GetWeaponType()),
            weaponCurrentAmountOfAmmo[GetWeaponIndex()], weaponS0.GetMaxAmountOfAmmo());

        canShot = false;
        timeFromLastShot = 0f;
    }

    bool HasAmmo()
    {
        return weaponCurrentAmountOfAmmo[GetWeaponIndex()] > 0;
    }

    int GetWeaponIndex()
    {
        for (int i = 0; i < weaponSos.Count; i++)
        {
            if (weaponSos[i].GetWeaponType() == _selectedWeaponType.GetWeaponType())
            {
                return i;
            }
        }

        return 0;
    }

    int GetWeaponIndexByTreeType(EarthTreeType typeSearched)
    {
        for (int i = 0; i < weaponSos.Count; i++)
        {
            if (weaponSos[i].GetWeaponType() == typeSearched)
            {
                return i;
            }
        }

        return 0;
    }

    #region Ammo

    void RemoveAmmo()
    {
        weaponCurrentAmountOfAmmo[GetWeaponIndex()] =
            weaponCurrentAmountOfAmmo[GetWeaponIndex()] - _selectedWeaponType.GetAmountOfAmmoToRemoveInEachShot();
        weaponCurrentAmountOfAmmo[GetWeaponIndex()] = Mathf.Max(0, weaponCurrentAmountOfAmmo[GetWeaponIndex()]);
    }

    #endregion

    #region ObjectPooling

    // Dequeue
    private void Shoot(string poolKey)
    {
        RemoveAmmo();

        Poolable p = GameObjectPoolController.Dequeue(poolKey);
        BulletController bulletController = p.GetComponent<BulletController>();
        bulletController.transform.position = bulletSpawnPosition.position;
        bulletController.transform.forward = bulletSpawnPosition.forward;
        bulletController.transform.localScale = Vector3.one;
        bulletController.gameObject.SetActive(true);
    }

    public void FullfillAmmo(EarthTreeType treeType)
    {
        weaponCurrentAmountOfAmmo[GetWeaponIndexByTreeType(treeType)] =
            weaponSos[GetWeaponIndexByTreeType(treeType)].GetMaxAmountOfAmmo();

        WeaponSO weaponS0 = weaponSos[GetWeaponIndexByTreeType(treeType)];
        _weaponSelectionBarUI.SetAmmoText(_weaponSelectionBarUI.GetWeaponIndex(weaponS0.GetWeaponType()),
            weaponCurrentAmountOfAmmo[GetWeaponIndex()], weaponS0.GetMaxAmountOfAmmo());

        Debug.Log("Fullfill");
    }

    #endregion
}