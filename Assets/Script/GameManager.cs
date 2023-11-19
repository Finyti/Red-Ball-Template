using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// Pattern - singleton

public class GameManager : MonoBehaviour
{
    public int hp;
    int hpReset;

    public int currentLevel;
    public List<string> levels;
    public static GameManager instance;

    public GameObject healthBar;
    public GameObject healthUnit;
    List<GameObject> healthList = new List<GameObject>();

    public GameObject coinsEnumeratorPrefab;
    GameObject coinsEnumerator;
    public List<int> coinsMemory;

    public AudioClip winAudio;
    public AudioClip looseAudio;
    public AudioClip minusLife;



    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(healthBar);

            coinsEnumerator = (GameObject)Instantiate(coinsEnumeratorPrefab, new Vector2(
                    -healthBar.transform.position.x, healthBar.transform.position.y), healthBar.transform.rotation);
            DontDestroyOnLoad(coinsEnumerator);
            coinsMemory.Add(0);

            hpReset = hp;

            for (int i = 0; i < hpReset; i++)
            {
                GameObject instObj = (GameObject)Instantiate(healthUnit, new Vector2(
                    healthBar.transform.position.x + (1 * healthList.Count), healthBar.transform.position.y), healthBar.transform.rotation);
                healthList.Add(instObj);
                DontDestroyOnLoad(instObj);
            }
        }
        else
        {
            print("SOMEBODY TO REPLACE YOURSELF!");
            Destroy(this);
        }
    }

    public void Win()
    {
        currentLevel++;
        if (currentLevel < levels.Count)
        {

            SceneManager.LoadScene(levels[currentLevel]);
            var source = GetComponent<AudioSource>();
            source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            source.volume = 100f;
            source.PlayOneShot(winAudio);
            int number = Int32.Parse(coinsEnumerator.GetComponent<TextMeshPro>().text);
            coinsMemory.Add(number);

        }
        else
        {
            SceneManager.LoadScene("WinScene");
            Clear();
        }

    }
    public void Loose()
    {
        hp--;


        if (hp > 0)
        {
            var source = GetComponent<AudioSource>();
            source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            source.volume = 100f;
            source.PlayOneShot(minusLife);

            Destroy(healthList[healthList.Count - 1]);
            healthList.RemoveAt(healthList.Count - 1);

            coinsEnumerator.GetComponent<TextMeshPro>().text = coinsMemory[coinsMemory.Count - 1].ToString();

            SceneManager.LoadScene(levels[currentLevel]);
        }
        else
        {
            var source = GetComponent<AudioSource>();
            DontDestroyOnLoad(source);
            source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            source.volume = 100f;
            source.PlayOneShot(looseAudio);
            SceneManager.LoadScene("LooseScene");
            Clear();
        }
    }
    public void Clear()
    {
        currentLevel = 0;
        hp = hpReset;
        if(healthList.Count > 0)
        {
            foreach (GameObject instObj in healthList)
            {
                Destroy(instObj);
            }
        }
        Destroy(healthBar);
        Destroy(coinsEnumerator);
        Destroy(this);
    }

    public void CoinAdd(GameObject coin)
    {
        Destroy(coin);
        int number = Int32.Parse(coinsEnumerator.GetComponent<TextMeshPro>().text) + 1;
        coinsEnumerator.GetComponent<TextMeshPro>().text = number.ToString();
    }

}
