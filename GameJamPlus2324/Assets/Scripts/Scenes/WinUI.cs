using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    public string mainMenuScene;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(mainMenuScene));
    }
}