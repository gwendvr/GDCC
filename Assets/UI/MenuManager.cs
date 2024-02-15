using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public PlayerSpawner playerSpawner;
    public Camera cam;
    public Button[] buttons;

    public void TwoPlayerSelected()
    {
        playerSpawner.SpawnPlayer(2);
        Destroy(cam);
        foreach (var button in buttons)
        {
            Destroy(button);
        }
        Destroy(this);
    }
    public void ThreePlayerSelected()
    {
        playerSpawner.SpawnPlayer(3);
        Destroy(cam);
        foreach (var button in buttons)
        {
            Destroy(button);
        }
        Destroy(this);
    }
    public void FourPlayerSelected()
    {
        playerSpawner.SpawnPlayer(4);
        Destroy(cam);
        foreach (var button in buttons)
        {
            Destroy(button);
        }
        Destroy(this);
    }
}
