using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : BaseMenu
{
    private void OnEnable()
    {
        //GameManager.PauseGame();
    }

    private void OnDisable()
    {
        //GameManager.UnpauseGame();
    }
    
    public void QuitToMenu()
    {
        Destroy(GameManager.GetGameManager().gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void ShowMenu()
    {
        isShown = true;
        gameObject.SetActive(true);
    }

    public override void HideMenu()
    {
        isShown = false;
        gameObject.SetActive(false);
    }
}
