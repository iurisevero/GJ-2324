using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [Header("Weapons")] [SerializeField] List<WeaponSO> weaponSos;
    private WeaponSO _selectedWeaponType;

    private bool canShot = true;
    const string GrapeBulletPoolKey = "GrapeBullet";
    const string AvocadoBulletPoolKey = "AvocadoBullet";
    const string StrawberryBulletPoolKey = "StrawberryBullet";
    const string BananaBulletPoolKey = "BananaBullet";

    [Header("Bullet")] private float timeToBulletDisappear = 1.25f;

    private float timeFromLastShot = 0f;

    private void Start()
    {
        _selectedWeaponType = weaponSos[0];
        GameObjectPoolController.AddEntry(GrapeBulletPoolKey, weaponSos[0].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(AvocadoBulletPoolKey, weaponSos[1].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(StrawberryBulletPoolKey, weaponSos[2].GetBulletPrefab(), 3, 15);
        GameObjectPoolController.AddEntry(BananaBulletPoolKey, weaponSos[3].GetBulletPrefab(), 3, 15);
    }

    void Update()
    {
        if (!canShot)
        {
            timeFromLastShot += Time.deltaTime;
            if (timeFromLastShot > _selectedWeaponType.GetCadence())
            {
                canShot = true;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(_selectedWeaponType);
        }
    }

    public void SwapWeapon(int swapedWeapon)
    {
        _selectedWeaponType = weaponSos[swapedWeapon];
    }

    void Shoot(WeaponSO weaponS0)
    {
        if (!weaponS0.HasAmmo()) return;

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

        canShot = false;
        timeFromLastShot = 0f;
    }

    #region ObjectPooling

    // Dequeue
    private void Shoot(string poolKey)
    {
        _selectedWeaponType.RemoveAmmo();

        Poolable p = GameObjectPoolController.Dequeue(poolKey);
        BulletController bulletController = p.GetComponent<BulletController>();
        bulletController.transform.position = transform.position;
        bulletController.transform.localScale = Vector3.one;
        bulletController.gameObject.SetActive(true);

        StartCoroutine(Enqueue(p.gameObject));
    }

    private IEnumerator Enqueue(GameObject bulletObject)
    {
        yield return new WaitForSeconds(timeToBulletDisappear);
        Poolable p = bulletObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    #endregion
}