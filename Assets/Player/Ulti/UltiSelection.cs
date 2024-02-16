using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiSelection : MonoBehaviour
{

    public PlayerController player;
    int UltiID;
    public GameObject buttons;

    public void OnSmokeSelected()
    {
        player.SetUltiID(0);
        buttons.SetActive(false);
    }
    public void OnSpiritVisionSelected()
    {
        player.SetUltiID(1);
        buttons.SetActive(false);
    }
    public void OnFakeCameraSelected()
    {
        player.SetUltiID(2);
        buttons.SetActive(false);
    }
    public void OnRevertCamSelected()
    {
        player.SetUltiID(3);
        buttons.SetActive(false);
    }
}
