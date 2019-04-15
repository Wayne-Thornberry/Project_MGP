using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtns : MonoBehaviour
{
    private void menuChoice(int i)
    {
        if (i == 0) // Main menu 
        {
            SceneManager.LoadScene("mainMenu");
        }

        if (i == 1) // First Menu for choosing which type of scenario
        {
            SceneManager.LoadScene("scenairosChoiceMenu");
        }

        if (i == 2) // Top speed and lane timming varaible setting menu 
        {
            SceneManager.LoadScene("variablesSettingMenu");
        }

        if (i == 3) // Algorithm varaible setting menu 
        {
            SceneManager.LoadScene("variablesSettingMenu2");
        }

        if(i == 4) // Open the scene TrafficSim
        {
            SceneManager.LoadScene("TrafficSim");
        }
    }

    public void NextScreen(int i) // Go to next menu
    {
        menuChoice(i + 1); // Current menu's next one
    }

    public void Goback(int i) // Go to previous menu
    {
        // if not on main menu
        if (i >= 0)
        {
            menuChoice(i - 1); // Current menu's previous one
        }
    }

    public void Quit() // quits when it's compiled (.exe)
    {
        Application.Quit();
    }
}