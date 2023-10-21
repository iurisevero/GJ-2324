using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionBarUI : MonoBehaviour
{
    [SerializeField] ShootManager shootManager;

    public List<WeaponSlot> weaponSlots = new List<WeaponSlot>();

    [SerializeField] private Sprite selectedWeaponImage;
    [SerializeField] private Sprite notSelectedWeaponImage;

    void Start()
    {
        SelectTheWeapon(shootManager._selectedWeaponType.GetWeaponType());
    }

    public void SelectTheWeapon(EarthTreeType treeType)
    {
        foreach (var weaponSlot in weaponSlots)
        {
            if (weaponSlot.treeType.Equals(treeType))
            {
                weaponSlot.image.sprite = selectedWeaponImage;
            }
            else
            {
                weaponSlot.image.sprite = notSelectedWeaponImage;
            }
        }
    }
}