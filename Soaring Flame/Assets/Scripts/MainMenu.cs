using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject CreditsMenu;
    public GameObject SettingsMenu;
    public GameObject ActiveMenu;
    // Start is called before the first frame update
    void Start()
    {
        ActiveMenu = mainMenu;
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ActiveMenu.SetActive(true);
    }
    public void SeeCredits()
    {
        ActiveMenu.SetActive(false);
        ActiveMenu = CreditsMenu;
        ActiveMenu.SetActive(true);
    }
    public void SeeSettings()
    {
        ActiveMenu.SetActive(false);
        ActiveMenu = SettingsMenu;
        ActiveMenu.SetActive(true);
    }
    public void BackToMain()
    {
        ActiveMenu.SetActive(false);
        ActiveMenu = mainMenu;
        ActiveMenu.SetActive(true);
    }
<<<<<<< Updated upstream
    public void PlayGame(string level)
=======
    public void PlayGame(string Scene)
>>>>>>> Stashed changes
    {
        V.Level = level;
        V.BaseHealth = 200;
<<<<<<< Updated upstream
        V.Wave = 0;
        SceneManager.LoadScene("SampleScene");
=======
        V.ActiveScene = Scene;
        SceneManager.LoadScene(Scene);
>>>>>>> Stashed changes
    }
}
