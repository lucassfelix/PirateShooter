using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnTime;
    [SerializeField] private GameObject gameSessionTime;

    public void PlayGame()
    {
        spawnTime.SetActive(true);
        gameSessionTime.SetActive(true);
        PlayerPrefs.SetFloat("SpawnTime",spawnTime.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("GameSession",gameSessionTime.GetComponent<Slider>().value);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
