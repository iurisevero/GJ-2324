using UnityEngine;

public class SwapWeaponManager : MonoBehaviour
{
    [SerializeField] private ShootManager shootManager;
    [SerializeField] private int numberOfWeapons = 4;
    private int currentWeaponIndex = 0;

    void Update()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(mouseWheel) > 0.0f)
        {
            // Lógica de troca de arma com a rolagem do mouse
            if (mouseWheel > 0)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % numberOfWeapons;
            }
            else
            {
                currentWeaponIndex = (currentWeaponIndex - 1) < 0? numberOfWeapons : currentWeaponIndex;
                currentWeaponIndex--;
            }
        }
        else
        {
            // Lógica de troca de arma com as teclas 1, 2, 3, 4
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeaponIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeaponIndex = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeaponIndex = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentWeaponIndex = 3;
            }
        }

        // Chame o método para trocar de arma com base no novo índice
        shootManager.SwapWeapon(currentWeaponIndex);
    }
}