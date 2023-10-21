using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private string sceneToBeLoaded = "Game";

    void Start()
    {
        playButton.onClick.AddListener(OpenGameScene);
        exitButton.onClick.AddListener(Application.Quit);
    }

    void OpenGameScene()
    {
        SceneManager.LoadScene(sceneToBeLoaded);
    }
}