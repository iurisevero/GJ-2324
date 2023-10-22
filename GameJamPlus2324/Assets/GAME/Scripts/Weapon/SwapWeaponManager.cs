using UnityEngine;

public class SwapWeaponManager : MonoBehaviour
{
    [SerializeField] private ShootManager shootManager;
    private int currentWeaponIndex = 0;

    void Update()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(mouseWheel) > 0.0f)
        {
            // Lógica de troca de arma com a rolagem do mouse
            if (mouseWheel > 0)
            {
                currentWeaponIndex++;
            }
            else
            {
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

        // Certifique-se de que o índice da arma não seja menor que 0 ou maior que o número de armas disponíveis
        currentWeaponIndex = Mathf.Clamp(currentWeaponIndex, 0, shootManager.weaponSos.Count - 1);

        // Chame o método para trocar de arma com base no novo índice
        shootManager.SwapWeapon(currentWeaponIndex);
    }
}