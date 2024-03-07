using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerController[] player;
    public GameObject[] buttonUltiSelection;
    public int nbPlayer;
    public Animator[] fakePhotoSpawnPoints;
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
        for (int i = nbPlayer - 1; i >= 0; i--)
        {
            if(i!=_exeption)
            {
                player[i].cam.transform.rotation = new Quaternion(0, player[i].cam.transform.rotation.y, 180, player[i].cam.transform.rotation.w);
                StartCoroutine(ResetScreen(i));
            }
        }

    }

    public void SpawnFakePhoto()
    {
        int _fakePhotoSelected1 = Random.Range(0, fakePhotoSpawnPoints.Length);
        int _fakePhotoSelected2 = Random.Range(0, fakePhotoSpawnPoints.Length);
        int _fakePhotoSelected3 = Random.Range(0, fakePhotoSpawnPoints.Length);
        int _fakePhotoSelected4 = Random.Range(0, fakePhotoSpawnPoints.Length);
        print("fake cams selected : " + _fakePhotoSelected1 + _fakePhotoSelected2 + _fakePhotoSelected3 + _fakePhotoSelected4);

        for(int i = 0; i < fakePhotoSpawnPoints.Length; i++) 
        {
            if (i == _fakePhotoSelected1 || i == _fakePhotoSelected2 || i == _fakePhotoSelected3 || i == _fakePhotoSelected4)
            {
                fakePhotoSpawnPoints[i].SetTrigger("Sortir");
                StartCoroutine(HideFakeCams(i));
            }
        }
    }

    IEnumerator ResetScreen(int _playerID)
    {
        yield return new WaitForSeconds(5f);
        player[_playerID].cam.transform.rotation = new Quaternion(0, player[_playerID].cam.transform.rotation.y, 0, 0);
    }

    IEnumerator HideFakeCams(int _fakeCamID)
    {
        yield return new WaitForSeconds(5f);
        fakePhotoSpawnPoints[_fakeCamID].SetTrigger("Ranger");
    }
    }
