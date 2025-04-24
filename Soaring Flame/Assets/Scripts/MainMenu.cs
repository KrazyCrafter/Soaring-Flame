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
    public void PlayGame(string level)
    {
        V.Level = level;
        V.BaseHealth = 200;
        V.Wave = 0;
        SceneManager.LoadScene("SampleScene");
    }
}
