using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController[] player;
    public void SpawnPlayer(int nbPlayer)
    {
        switch(nbPlayer)
        {
            case 2:
                player[2].destroy();
                player[3].destroy();
                player[0].AdjustCam(0);
                player[1].AdjustCam(0.5f);
                break; 

            case 3:
                player[3].destroy();
                break;

            case 4:
                break;

        }
    }
}
