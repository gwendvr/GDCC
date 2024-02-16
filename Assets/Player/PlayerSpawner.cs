using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController[] player;
    public GameObject[] buttonUltiSelection;
    public int nbPlayer;
    public void SpawnPlayer(int _nbPlayer)
    {
        nbPlayer = _nbPlayer;
        switch(nbPlayer)
        {
            case 2:
                buttonUltiSelection[2].SetActive(false);
                buttonUltiSelection[3].SetActive(false);
                player[2].destroy();
                player[3].destroy();
                player[0].AdjustCam(0);
                player[1].AdjustCam(0.5f);
                break; 

            case 3:
                buttonUltiSelection[3].SetActive(false);
                player[3].destroy();
                break;

            case 4:
                break;

        }
    }

    public void InvertCam(int _exeption)
    {
        for (int i = nbPlayer; i >= 0; i--)
        {
            if(i!=_exeption)
            {
                player[i].cam.transform.rotation = Quaternion.Euler(player[i].cam.transform.rotation.x, player[i].cam.transform.rotation.y, 180);
                StartCoroutine(ResetScreen(i));
            }
        }

    }

    IEnumerator ResetScreen(int _playerID)
    {
        yield return new WaitForSeconds(5f);
        player[_playerID].cam.transform.Rotate(180, 0, 0);
    }
}
