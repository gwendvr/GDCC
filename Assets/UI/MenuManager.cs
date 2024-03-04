using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public PlayerSpawner playerSpawner;
    public Camera cam;
    public Button[] buttons;
    public GameObject setting;
    public GameObject menu;

    void Start()
    {
        menu.SetActive(true);
        setting.SetActive(false);
    }

    public void TwoPlayerSelected()
    {
        playerSpawner.SpawnPlayer(2);
        Destroy(cam);
        menu.SetActive(false);
    }
    public void ThreePlayerSelected()
    {
        playerSpawner.SpawnPlayer(3);
        Destroy(cam);
        menu.SetActive(false);
    }
    public void FourPlayerSelected()
    {
        playerSpawner.SpawnPlayer(4);
        Destroy(cam);
        menu.SetActive(false);
    }

    public void GoSetting()
    {
        menu.SetActive(false);
        setting.SetActive(true);
    }

    public void ReturnMenu()
    {
        menu.SetActive(true);
        setting.SetActive(false);
    }
}
