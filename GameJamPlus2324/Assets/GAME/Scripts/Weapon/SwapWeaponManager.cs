using UnityEngine;

public class SwapWeaponManager : MonoBehaviour
{
    [SerializeField] private ShootManager shootManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootManager.SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootManager.SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootManager.SwapWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shootManager.SwapWeapon(3);
        }
    }
}