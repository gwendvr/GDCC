using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public float sensitivity = 5f;
    public Camera cam;
    Vector2 moveCam;
    public bool onPhotoMode = false;
    public GameObject photoDevice;
    public Animator photoAnim;
    public UltiBehaviour ulti;
    public int ID; 


    private void Start()
    {

        //UnityEngine.Cursor.lockState = CursorLockMode.Locked; 
        //UnityEngine.Cursor.visible = false;  
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, moveCam.y  * sensitivity * Time.deltaTime, 0);
        Mathf.Clamp(moveCam.x, -80, 80);
        cam.transform.Rotate(moveCam.x * sensitivity * Time.deltaTime, 0, 0);
        moveCam = new Vector2(0, 0);
    }


    public void OnMoveCamUpDown(InputValue value)
    {
        moveCam.x = moveCam.x + value.Get<float>();
    }

    public void OnMoveCamLeftRight(InputValue value)
    {
        moveCam.y = moveCam.y + value.Get<float>();
    }


    public void OnPhotoMode()
    {

        onPhotoMode = !onPhotoMode;
        if(onPhotoMode)
        {
            photoAnim.SetTrigger("Sortir");
            // Ajouter l'autorisation de prise de photo
        }
        else
        {
            photoAnim.SetTrigger("Ranger");
            //Retirer l'autorisation de prise de photo
        }
    }

    public void AdjustCam(float xPlacement)
    {
        cam.rect = new Rect(xPlacement, 0, 0.5f, 1);
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    public void SetUltiID(int _ultiID)
    {
        ulti.SetUltiID(_ultiID, ID);
    }

    public void OnAddUltiProgression()
    {
        ulti.AddUltiProgression(10);
        
    }

    public void OnUlti()
    {
        ulti.UseUlti(transform.position);
    }

}
