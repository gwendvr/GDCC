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
        transform.Rotate(0, moveCam.x  * sensitivity * Time.deltaTime, 0);

        float pitch = -moveCam.y * sensitivity * Time.deltaTime;
        float pitchValue = (cam.transform.rotation.eulerAngles.x + 180.0f) % 360.0f - 180.0f;
        float yRotation = Mathf.Clamp(pitchValue + pitch, -40.0f, 40.0f) - cam.transform.rotation.eulerAngles.x;
        cam.transform.Rotate(yRotation, 0, 0);
        //moveCam = new Vector2(0, 0);
    }


    public void OnMoveCam(InputValue value)
    {
        Debug.Log(value);

        moveCam = value.Get<Vector2>();
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
