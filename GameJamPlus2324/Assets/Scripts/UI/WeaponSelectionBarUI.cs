using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSelectionBarUI : MonoBehaviour
{
    [SerializeField] ShootManager shootManager;

    public List<WeaponSlot> weaponSlots = new List<WeaponSlot>();

    [SerializeField] private Sprite selectedWeaponImage;
    [SerializeField] private Sprite notSelectedWeaponImage;

    public List<TextMeshProUGUI> ammoTexts;

    public void SetAmmoText(int index, int currentAmmo, int maxAmmo)
    {
        ammoTexts[index].text = currentAmmo + "/" + maxAmmo;
    }

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

    public int GetWeaponIndex(EarthTreeType treeType)
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (weaponSlots[i].treeType.Equals(treeType))
            {
                return i;
            }
        }

        return 0;
    }
}