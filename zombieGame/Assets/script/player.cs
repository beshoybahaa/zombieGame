using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI textMesh;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        textMesh.text= $"health : {HP}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("player die");
            playerDead();
            textMesh.gameObject.SetActive(false);
            isDead = true;
        }
        else
        {
            print("player damage");
            StartCoroutine(bloodyScreenEffect());
            textMesh.text = $"health : {HP}";
        }
    }

    private void playerDead()
    {
        GetComponent<mouseMovement>().enabled=false;
        GetComponent<playerMovement>().enabled=false;
        GetComponentInChildren<Animator>().enabled=true;
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
        print("collisotino");
        if(other.CompareTag("zombieHand")){
            TakeDamage(25);
        }}
    }
}
