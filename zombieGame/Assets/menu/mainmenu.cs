using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenu : MonoBehaviour
{
    public TMP_Text highscoreui;
    string newgamescene = "samplescene";
    void Start()
    {
    }
    public void startnewgame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitapplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

#endif
    }
}
