using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;

    public string mainMenuScene;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(mainMenuScene));
        exitButton.onClick.AddListener(Application.Quit);
    }
}