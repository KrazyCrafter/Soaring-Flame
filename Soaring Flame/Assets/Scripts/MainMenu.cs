using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject CreditsMenu;
    public GameObject ActiveMenu;
    // Start is called before the first frame update
    void Start()
    {
        V.BaseHealth = 100;
        ActiveMenu = mainMenu;
        CreditsMenu.SetActive(false);
        ActiveMenu.SetActive(true);
    }
    public void SeeCredits()
    {
        ActiveMenu.SetActive(false);
        ActiveMenu = CreditsMenu;
        ActiveMenu.SetActive(true);
    }
    public void BackToMain()
    {
        ActiveMenu.SetActive(false);
        ActiveMenu = mainMenu;
        ActiveMenu.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
