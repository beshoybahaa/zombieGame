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
    static int deadCount=0;
    public int afterwave;
    // public List<GameObject> spwanList;
    static int level=1;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            StartCoroutine(WaveCoolDown());
        }

        if(inCoolDown){
            coolDownCounter -= Time.deltaTime;
        }else{
            coolDownCounter = waveCoolDown;
        }
        deadCountUI.text = "score : " + deadCount;
        if(level == 1 && currentWave>afterwave){
            SceneManager.LoadScene(1);
            level =2;
        }

    }

    private IEnumerator WaveCoolDown()
    {
        inCoolDown = true;
        yield return new WaitForSeconds(waveCoolDown);
        inCoolDown = false;
        currentZombiePerWave *=2;
        StartNextWav();
    }
}
