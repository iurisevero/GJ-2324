using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadController : MonoBehaviour
{
    private TreeSpawnerAreaController currentTreeSpawnerAreaController;
    private WeaponSO currentWeapon;
    private bool reloading = false;
    private float timeToRefill = 0;

    public GameObject reloadObj;
    public Image reloadFill;
    public IEnumerator fillCoroutine;

    private void OnPlayerExitPlantationAreaEvent(PlayerExitPlantationAreaEvent evt) => 
        OnPlayerExitPlantationAreaHandler();
    
    public void Awake()
    {
        EventManager.AddListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaHandler);
        EventManager.AddListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
    }

    void Start()
    {
        reloading = false;
        timeToRefill = 0;
        reloadObj.SetActive(false);
    }

    void Update()
    {
        if(reloading)
        {
            reloadFill.fillAmount += 1.0f / currentWeapon.GetTimeToFullrefill() * Time.deltaTime;
            timeToRefill -= Time.deltaTime;
            if(timeToRefill  < 0)
            {
                Reload();
                HideReloadUI();
                reloading = false;
            }
        }
    }

    private void Reload()
    {
        Player.Instance.Reload(currentTreeSpawnerAreaController.plantedTreeType);
        Debug.Log("Finish reload");
    }

    public void ShowReloadUI(float reloadTime)
    {
        reloadObj.SetActive(true);
        reloadFill.fillAmount = 0;
    }

    public void HideReloadUI()
    {
        reloadObj.SetActive(false);
    }

    private void OnPlayerEnterPlantationAreaHandler(PlayerEnterPlantationAreaEvent evt)
    {
        Debug.Log("OnPlayerEnterPlantationAreaHandler");
        currentTreeSpawnerAreaController = evt.treeSpawnerAreaController;
        if(currentTreeSpawnerAreaController.planted)
        {
            currentWeapon = Player.Instance.GetCurrentWeapon();
            if(currentWeapon.GetWeaponType() == currentTreeSpawnerAreaController.plantedTreeType)
            {
                timeToRefill = currentWeapon.GetTimeToFullrefill();
                ShowReloadUI(timeToRefill);
                reloading = true;
                Debug.Log("Starting reload");
            }

        }
    }

    private void OnPlayerExitPlantationAreaHandler()
    {
        // Debug.Log("OnPlayerExitPlantationAreaHandler");
        currentTreeSpawnerAreaController = null;
        currentWeapon = null;
        HideReloadUI();
    }

    public void OnDestroy()
    {
        EventManager.RemoveListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaHandler);
        EventManager.RemoveListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
    }
}
