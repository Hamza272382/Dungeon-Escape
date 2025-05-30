using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the Start button is pressed
    // Loads the scene with index 1 (usually the main gameplay scene)
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    // Called when the Quit button is pressed
    // Quits the application (only works in a built game, not in the editor)
    public void QuitButton()
    {
        Application.Quit();
    }
}
