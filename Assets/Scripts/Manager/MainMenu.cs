using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button countinueGameButton;

    public void PlayGame()
    {
        DisableMenuButtons();
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void Continue()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        countinueGameButton.interactable = false;
    }
}
