using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiBehaviour : MonoBehaviour
{

    public GameObject smokeVFX;
    public PlayerSpawner playerGestion;
    public int playerID;

    private int ultiID;
    private float ultiProgression;
    
    
    public void SetUltiID(int _ID, int _playerID)
    {
        ultiID = _ID;
        playerID = _playerID;
    }
    public void AddUltiProgression(float _progressionToAdded)
    {
        ultiProgression += _progressionToAdded;
        print(ultiProgression);
    }

    public void UseUlti(Vector3 _position)
    {
        if (ultiProgression >= 100)
        {
            switch(ultiID)
            {
                case 0: //Smoke
                    print("create smoke");
                    var smoke = Instantiate(smokeVFX, new Vector3(_position.x, _position.y - 1, _position.z), Quaternion.Euler(-90,0,0));
                    break;

                case 1: //Spirit Vision
                    print("active spirit vision");
                    
                    break;

                case 2: //Fake Camera
                    print("spawn fake camera");

                    break;

                case 3: //Revert Cam
                    print("invert cameras");
                    playerGestion.InvertCam(playerID);
                    break;
            }
            ultiProgression = 0;
        }
        else print("not enought progression");

    }
}
