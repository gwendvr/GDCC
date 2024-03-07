using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public int id;
    public int score;
    public GameObject UIInteract;
    public GameObject textDone;
    public Slider progressBar;
    public bool canInteract;
    public bool tryToInteract;
    public PlayerController playerController;
    //public bool isInteracting;
    public bool isChargingPhoto;
    public List<GameObject> listOtherPlayer;
    public List<SkinnedMeshRenderer> myMeshRenderer;
    private IInteract currentInteraction;
    public Camera myCamera;
    public int cameraReach;
    public bool isAlive;
    public bool cooldownPhoto;
    public bool isFlashing;
    public Light flash;


    void Start()
    {
        UIInteract.SetActive(false);
        progressBar.gameObject.SetActive(false);
        canInteract = false;
        //isInteracting = false;
        score = 0;
        isAlive=true;
        isChargingPhoto = true;
        cooldownPhoto = true;
        isFlashing = false;
        flash.intensity = 0;
    }

    void Update()
    {

        if (isAlive == false)
        {
            return;
        }
        if (canInteract && Input.GetKey(KeyCode.E))
        {
            if (currentInteraction.available == true)

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
        if ( isChargingPhoto&& Input.GetMouseButtonDown(0)&&cooldownPhoto)
        {
            takePhoto();
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

    public void takePhoto()
    {
        for(int i=0; i < listOtherPlayer.Count; i++)
        {
            player otherplayer = listOtherPlayer[i].GetComponentInChildren<player>();
            Vector3 pos = myCamera.WorldToViewportPoint(otherplayer.myMeshRenderer[0].transform.position);
            RaycastHit rh = new RaycastHit();
            bool rc = Physics.Raycast(this.transform.position, otherplayer.transform.position - this.transform.position, out rh,100,512);
            if (rc == true) {
                Debug.Log("Raycast colliding with " + rh.collider.gameObject.tag);
                if (pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1 && pos.z > 0 && Mathf.Abs((otherplayer.transform.position - this.transform.position).magnitude) < cameraReach && rh.collider.gameObject.tag.Equals("Player"))
                {
                    otherplayer.die();
                    StartCoroutine(flashingTarget(otherplayer));
                }
            }
            
        }
        StartCoroutine(flashing());
        StartCoroutine(cooldownNewPhoto());
    }

    public void die()
    {
        isAlive = false;
        canInteract=false;
        flash.intensity = 0;
        progressBar.value = 0;
    }

    IEnumerator cooldownNewPhoto()
    {
        cooldownPhoto = false;
        yield return new WaitForSeconds(5);
        cooldownPhoto = true;
    }

    IEnumerator flashing()
    {
        isFlashing = true;
        flash.intensity = 10;
        yield return new WaitForSeconds(1.5f);
        flash.intensity = 0;
        isFlashing = false;
    }
    IEnumerator flashingTarget(player p)
    {
        isFlashing = true;
        flash.intensity = 10;
        yield return new WaitForSeconds(1.5f);
        flash.intensity = 0;
        p.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        isFlashing = false;
    }

    /*private void setupOtherPlayers()
    {
        listOtherPlayer = new List<GameObject>();
        int nbPlayers= players.transform.childCount;
        for(int i = 0; i < nbPlayers; i++)
        {
            Debug.Log("player id: " + players.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<player>().id);
            if (players.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<player>().id!=this.id)
            {
                listOtherPlayer.Add(players.transform.GetChild(i).gameObject);
            }
        }
    }*/
}
