using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenu : MonoBehaviour
{
    void Start()
    {
    }
    public void startnewgame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitapplication()
    {
        print("exit");
        Application.Quit();
    }
}
