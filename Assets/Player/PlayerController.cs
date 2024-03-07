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
    public float sensitivity = 20f;
    public Camera cam;
    Vector2 moveCam;
    public bool onPhotoMode = false;
    public GameObject photoDevice;
    public Animator photoAnim;
    public UltiBehaviour ulti;
    public int ID;
    public player playerInteraction;

    //Stay Task Variable
    private StayTaskPoint stayPoint;
    private bool inStayTask = false;

    public FirstPersonController firstPersonController;

    private void Start()
    {

        //UnityEngine.Cursor.lockState = CursorLockMode.Locked; 
        //UnityEngine.Cursor.visible = false;  
    }

    private void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, moveCam.x * sensitivity * Time.deltaTime, 0);
        cam.transform.localEulerAngles -= new Vector3(moveCam.y * sensitivity * Time.deltaTime, 0, 0);
        /*
        //transform.Rotate(0, moveCam.y  * sensitivity * Time.deltaTime, 0);
        if (moveCam.x < 0 && cam.transform.rotation.x < 80)
        {
            //cam.transform.Rotate(moveCam.x * sensitivity * Time.deltaTime, 0, 0);
        }
        else if (moveCam.x > 0 && cam.transform.rotation.x > -80)
        {
            cam.transform.localEulerAngles = new Vector3(moveCam.x, 0, 0);
            //cam.transform.Rotate(moveCam.x * sensitivity * Time.deltaTime, 0, 0);
        }
        //moveCam = new Vector2(0, 0);*/

        if(inStayTask)
        {
            if (stayPoint.timeToStay <= 0)
            {
                ulti.AddUltiProgression(stayPoint.ultiGain);
                inStayTask = false;
                Destroy(stayPoint.gameObject);
                
            }
            else stayPoint.timeToStay -= Time.deltaTime;
        }

    }

    public void OnMoveCam(InputValue inputValue)
    {
        moveCam = inputValue.Get<Vector2>();
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

    public void OnAddUltiProgression(float _percent)
    {
        ulti.AddUltiProgression(_percent);
        
    }

    public void OnUlti()
    {
        ulti.UseUlti(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Staytask"))
        {
            StayTaskPoint _stayPoint = collision.gameObject.GetComponent<StayTaskPoint>();
            inStayTask = true;
            stayPoint = _stayPoint;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Staytask"))
        {
            StayTaskPoint _stayPoint = collision.gameObject.GetComponent<StayTaskPoint>();
            inStayTask = false;
            stayPoint.timeToStay = stayPoint.timeReset;
            stayPoint = null;
        }
    }

    public void FreezePlayer(bool _canMove)
    {
        firstPersonController.playerCanMove = !_canMove;
    }
}
