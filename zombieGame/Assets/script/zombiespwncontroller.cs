using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class zombiespwncontroller : MonoBehaviour
{
    public int initialZombiesPerWave = 10;
    public int currentZombiePerWave;

    public float spwanDelay = 0.5f;

    public int currentWave = 0;
    public float waveCoolDown = 10f;

    public bool inCoolDown;
    public float coolDownCounter = 0;

    public List<zombie> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI currentWaveUI;
    public TextMeshProUGUI deadCountUI;
    public TextMeshProUGUI winUI;
    public TextMeshProUGUI toNextLevelUI;
    public TextMeshProUGUI countingUI;
    public TextMeshProUGUI toMenueUI;
    static int deadCount=0;
    public int afterwave;
    // public List<GameObject> spwanList;
    static int level=1;
    static bool end=false;
    private bool finish = false;
    List<Transform> wayPointsList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
     currentZombiePerWave = initialZombiesPerWave;
     StartNextWav();
    }

    private void StartNextWav()
    {
            currentZombiesAlive.Clear();
            currentWave++;
            currentWaveUI.text= "Wave : " + currentWave;
            StartCoroutine(SpawnWave());
        
    }

    private IEnumerator SpawnWave()
    {
        if(end){}else{
        for (int i = 0; i<currentZombiePerWave; i++){

            Vector3 spawnOffeset = new Vector3(UnityEngine.Random.Range(-1f,1f),0f, UnityEngine.Random.Range(-1f, 1f));
            GameObject zombiespawner = GameObject.FindGameObjectWithTag("zombiespawner");
            foreach (Transform t in zombiespawner.transform)
            {
                wayPointsList.Add(t);
            }

            Vector3 spawnPosition = wayPointsList[UnityEngine.Random.Range(0, wayPointsList.Count)].position + spawnOffeset;

            var zombie = Instantiate(zombiePrefab,spawnPosition,Quaternion.identity);

            zombie zombieScript = zombie.GetComponent<zombie>();

            currentZombiesAlive.Add(zombieScript);

            yield return new WaitForSeconds(spwanDelay);
        }}
    }

    // Update is called once per frame
    void Update()
    {
        deadCountUI.text = "score : " + deadCount;
        if (end)
        {
            level = 0;
            SceneManager.LoadScene(2);
        }
        print(currentWave);
        if (level == 1 && currentWave > afterwave)
        {
            SceneManager.LoadScene(0);
            level = 2;
        }
        if (end && currentWave == afterwave)
        {}else{
        List<zombie> zombieToRemove = new List<zombie>();
        foreach(zombie zombie in currentZombiesAlive){
            if(zombie.isDead){
                zombieToRemove.Add(zombie);
                deadCount++;
            }
        }

        foreach(zombie zombie in zombieToRemove){
            currentZombiesAlive.Remove(zombie);
        }

        zombieToRemove.Clear();

        if(currentZombiesAlive.Count == 0 && inCoolDown == false){
            StartCoroutine(WaveCoolDown(waveCoolDown));
        }

        if(inCoolDown){
            coolDownCounter -= Time.deltaTime;
            if(currentWave ==afterwave&&level==1){
            toNextLevelUI.gameObject.SetActive(true);
            countingUI.gameObject.SetActive(true);
            if (coolDownCounter > 0)
                countingUI.text = (Mathf.FloorToInt(coolDownCounter % 60)).ToString();
            else
                countingUI.text = "0";
                }
            if(currentWave == afterwave && level == 2){
                    toMenueUI.gameObject.SetActive(true);
                    countingUI.gameObject.SetActive(true);
                    if(coolDownCounter>0)
                    countingUI.text = (Mathf.FloorToInt(coolDownCounter % 60)).ToString();
                    else
                    countingUI.text ="0";
                        winUI.text = "winner winner chicken dinner";
                    StartCoroutine("Finish");
                }
        }else{
                coolDownCounter = waveCoolDown;
        }}
        if(finish){
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(2);
            
        }
    }

    private IEnumerator WaveCoolDown(float waveCoolDown)
    {
        inCoolDown = true;
        yield return new WaitForSeconds(waveCoolDown);
        inCoolDown = false;
        currentZombiePerWave *=2;
        StartNextWav();
    }

    private IEnumerator Finish(){
        yield return new WaitForSeconds(10);
        finish=true;
    }
}
