using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    private void Start()
    {
        DataPersistanceManager.instance.NewGame();
    }
    public void ReturnMenu()
    {
        SceneManager.LoadSceneAsync("Main");
    }
}
