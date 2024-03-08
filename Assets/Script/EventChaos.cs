using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventChaos : MonoBehaviour
{
    public Light[] LightMap;
    public List<GameObject> Bot, PlayerCam;
    public Text Txt;

    private bool EventOnGoing = false;
    private int EventId = -1;

    public float magnitude = 0.1f;
    public float intensity = 5f;
    public float rotationIntensity = 2f;
    string eventName = null;
    

    // Update is called once per frame
    void Update()
    {
        if (!EventOnGoing)
        {
            
            EventId = Random.Range(0, 8);
            
            switch (EventId)
            {
                case 0:
                    Debug.Log("Event en court : DarkEvent " +EventId);
                    eventName = "Il va faire tout noir !";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(DarkEvent());
                    break;
                case 1:
                    Debug.Log("Event en court : SpeedEvent " +EventId);
                    eventName = "Rapide comme l'éclair";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(SpeedEvent());
                    break;
                case 2:
                    Debug.Log("Event en court : Earthquake " +EventId);
                    eventName = "C'est la danse de la Terre";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(Earthquake());
                    break;
                case 3:
                    Debug.Log("Event en court : DrunkEffectCoroutine " +EventId);
                    eventName = "Nan gé pas buuuuu !";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    foreach (GameObject cam in PlayerCam)
                    {
                        StartCoroutine(DrunkEffectCoroutine(cam)); // Démarre l'effet pour chaque caméra
                    }
                    break;
                case 4:
                    Debug.Log("Event en court : Nothing " +EventId);
                    eventName = "Event not found";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                case 5:
                    Debug.Log("Event en court : Nothing " +EventId);
                    eventName = "Event not found";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                case 6:
                    Debug.Log("Event en court : Nothing " +EventId);
                    eventName = "Event not found";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                case 7:
                    Debug.Log("Event en court : SlowMotion " +EventId);
                    eventName = "Mode retraités, experts en lenteur ";
                    StartCoroutine(ShowEventName());
                    EventOnGoing = true;
                    StartCoroutine(SlowMotion());
                    break;
            }

        }
    }
    
    IEnumerator ShowEventName()
    {
        Txt.gameObject.SetActive(true);
        Txt.text = eventName; 
        yield return new WaitForSeconds(2f);
        Txt.text = "";
        Txt.gameObject.SetActive(false);
    }

    IEnumerator DarkEvent()
    {
        for (int i = 0; i < LightMap.Length; i++)
        {
            LightMap[i].intensity = 0;
        }
        yield return new WaitForSeconds(30);
        for (int i = 0; i < LightMap.Length; i++)
        {
            LightMap[i].intensity = 1;
        }

        EventOnGoing = false;
    }

    IEnumerator SpeedEvent()
    {
        for (int i = 0; i < Bot.Count; i++)
        {
            Bot[i].GetComponent<IA>().speedMultiplier += 20;
        }
        yield return new WaitForSeconds(30);
        for (int i = 0; i < Bot.Count; i++)
        {
            Bot[i].GetComponent<IA>().speedMultiplier -= 20;
        }
        EventOnGoing = false;
    }

    IEnumerator SlowMotion()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(15);
        Time.timeScale = 1f;
        EventOnGoing = false;
    }

    IEnumerator Earthquake()
    {
        List<Vector3> originalPositions = new List<Vector3>();

        foreach (GameObject cam in PlayerCam)
        {
            originalPositions.Add(cam.transform.position);
        }

        float elapsed = 0.0f;

        while (elapsed < 30)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            for (int i = 0; i < PlayerCam.Count; i++)
            {
                GameObject cam = PlayerCam[i];
                Vector3 originalPosition = originalPositions[i];
                cam.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Remettre les caméras à leur position originale
        for (int i = 0; i < PlayerCam.Count; i++)
        {
            GameObject cam = PlayerCam[i];
            Vector3 originalPosition = originalPositions[i];
            cam.transform.position = originalPosition;
        }
            EventOnGoing = false;
        }
        IEnumerator DrunkEffectCoroutine(GameObject cam)
        {
            float startTime = Time.time;
            Vector3 originalPosition = cam.transform.localPosition; 
            Quaternion originalRotation = cam.transform.localRotation; 

            while (Time.time < startTime + 30)
            {
                
                float drunkX = Mathf.Sin(Time.time * 2) * intensity;
                float drunkY = Mathf.Cos(Time.time * 2.5f) * intensity;
                cam.transform.localPosition = originalPosition + new Vector3(drunkX, drunkY, 0);

                
                float rotationZ = Mathf.Sin(Time.time) * rotationIntensity;
                cam.transform.localRotation = originalRotation * Quaternion.Euler(0, 0, rotationZ);

                yield return null; 
            }
        
            
            cam.transform.localPosition = originalPosition;
            cam.transform.localRotation = originalRotation;
            EventOnGoing = false;
        }

        IEnumerator Nothing()
        {
            yield return new WaitForSeconds(30);
            EventOnGoing = false;
        }
        public void TwoPlayer()
        {
            PlayerCam.RemoveAt(3);
            PlayerCam.RemoveAt(2);
        }
        public void ThreePlayer()
        {
            PlayerCam.RemoveAt(3);
        }
}
