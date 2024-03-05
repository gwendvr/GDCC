using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    protected static int allId=0;
    protected int id;
    public int score;
    public GameObject UIInteract;
    public GameObject textDone;
    public Slider progressBar;
    public bool canInteract;
    public bool tryToInteract;
    public PlayerController playerController;
    //public bool isInteracting;
    private IInteract currentInteraction;

    void Start()
    {
        UIInteract.SetActive(false);
        progressBar.gameObject.SetActive(false);
        canInteract = false;
        //isInteracting = false;
        allId += 1;
        id = allId;
        score = 0;
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.JoystickButton0))
        {
            progressBar.gameObject.SetActive(true);
            //isInteracting = true;
            progressBar.value += 0.025f;
            if (progressBar.value == progressBar.maxValue)
            {
                clearTask();
                canInteract=false;
            }
        }
        else
        {
            textDone.gameObject.SetActive(true);
        }
        if (!Input.GetKey(KeyCode.JoystickButton0))
        {
            progressBar.gameObject.SetActive(false);
            textDone.gameObject.SetActive(false);
            progressBar.value = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IInteract>(out IInteract interact))
        {
            UIInteract.SetActive(true);
            canInteract = true;
            progressBar.maxValue = interact.ct;
            currentInteraction = interact;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (canInteract)
        {
            if (other.gameObject.TryGetComponent<IInteract>(out IInteract interact))
            {
                UIInteract.SetActive(false);
                canInteract = false;
            }
        }
    }
    
    //ce qu'il se passe qu'on finis une tache avec interaction
    public void clearTask()
    {
        currentInteraction.available = false; 
        progressBar.gameObject.SetActive(false);
        playerController.OnAddUltiProgression(progressBar.maxValue);
    }
}
