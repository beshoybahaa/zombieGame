using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI gameOverUI;
    public TextMeshProUGUI countingUI;
    public TextMeshProUGUI toMenueUI;
    private bool isDead = false;
    private bool startCounting = false;
    public float countTime;
    // Start is called before the first frame update
    void Start()
    {
        healthUI.text= $"health : {HP}";
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounting){
            if(isDead){
                toMenueUI.gameObject.SetActive(true);
            }
            if (countTime >= 0)
            {
                countingUI.gameObject.SetActive(true);
                countTime -= Time.deltaTime; // Update remaining time
                countingUI.text= (Mathf.FloorToInt(countTime % 60)).ToString(); // Update the UI Text with remaining time
            }
            else
            {
                // Handle what happens when the timer reaches zero (e.g., trigger events)
            }
        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("player die");
            playerDead();
            healthUI.gameObject.SetActive(false);
            isDead = true;
            gameOverUI.text = "Game Over";
        }
        else
        {
            print("player damage");
            StartCoroutine(bloodyScreenEffect());
            healthUI.text = $"health : {HP}";
        }
    }

    private void playerDead()
    {
        GetComponent<mouseMovement>().enabled=false;
        GetComponent<playerMovement>().enabled=false;
        GetComponentInChildren<Animator>().enabled=true;
        Cursor.lockState = CursorLockMode.None;
        startCounting = true;
        StartCoroutine(countDownTOMenu());
        
    }

    private IEnumerator countDownTOMenu(){
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(2);
    }

    private IEnumerator bloodyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy == false){
            bloodyScreen.SetActive(true);
        }
        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }
        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDead == false){
        if(other.CompareTag("zombieHand")){
            TakeDamage(25);
        }}
    }
}
