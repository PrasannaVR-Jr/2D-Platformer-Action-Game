using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapCounterMain : MonoBehaviour
{
    public void GoToLevel()
    {
        SceneManager.LoadScene("TapCounter");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
