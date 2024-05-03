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
        int highscore = savemanager.Instance.loadhighscore();
        highscoreui.text = $"top score: {highscore}";
    }
    public void startnewgame()
    {
        SceneManager.LoadScene(newgamescene);
    }

    public void exitapplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

#endif
    }
}
