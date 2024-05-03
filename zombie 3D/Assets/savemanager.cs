using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savemanager : MonoBehaviour
{
    public static savemanager Instance { get; set; }

     string highscorekey = "bestwavesavedvalue";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void savehighscore(int score)
    {
        PlayerPrefs.GetInt(highscorekey, score);

    }
    public int loadhighscore()
    {
        if (PlayerPrefs.HasKey(highscorekey))
        {
            return PlayerPrefs.GetInt(highscorekey);
        }
        else
        {
            return 0;
        }
    }
}
