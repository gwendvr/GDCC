using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChaos : MonoBehaviour
{
    public Light[] LightMap;
    public GameObject[] Bot, Player;

    private bool EventOnGoing = false;
    private int EventId = -1;

    public float magnitude = 0.1f;
    public float intensity = 5f;
    public float rotationIntensity = 2f;
    

    // Update is called once per frame
    void Update()
    {
        if (!EventOnGoing)
        {
            EventId = Random.Range(0, 7);
            Debug.Log("Event en court :" +EventId);
            switch (EventId)
            {
                case 0:
                    EventOnGoing = true;
                    StartCoroutine(DarkEvent());
                    break;
                case 1:
                    EventOnGoing = true;
                    StartCoroutine(SpeedEvent());
                    break;
                case 2:
                    EventOnGoing = true;
                    StartCoroutine(Earthquake());
                    break;
                case 3:
                    EventOnGoing = true;
                    foreach (GameObject cam in Player)
                    {
                        StartCoroutine(DrunkEffectCoroutine(cam)); // Démarre l'effet pour chaque caméra
                    }
                    break;
                case 4:
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                case 5:
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                case 6:
                    EventOnGoing = true;
                    StartCoroutine(Nothing());
                    break;
                
                    
            }
        }
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
        for (int i = 0; i < Bot.Length; i++)
        {
            Bot[i].GetComponent<IA>().speedMultiplier += 20;
        }
        yield return new WaitForSeconds(30);
        for (int i = 0; i < Bot.Length; i++)
        {
            Bot[i].GetComponent<IA>().speedMultiplier -= 20;
        }
        EventOnGoing = false;
    }
     IEnumerator Earthquake()
        {
            List<Vector3> originalPositions = new List<Vector3>(); 

            
            foreach (GameObject cam in Player)
            {
                originalPositions.Add(cam.transform.position);
            }

            float elapsed = 0.0f;

            while (elapsed < 30)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                for (int i = 0; i < Player.Length; i++)
                {
                    GameObject cam = Player[i];
                    Vector3 originalPosition = originalPositions[i];
                    cam.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Remet les caméras à leur position originale
            for (int i = 0; i < Player.Length; i++)
            {
                GameObject cam = Player[i];
                cam.transform.position = originalPositions[i];
            }
            EventOnGoing = false;
        }
        IEnumerator DrunkEffectCoroutine(GameObject cam)
        {
            float startTime = Time.time;
            Vector3 originalPosition = cam.transform.localPosition; // Sauvegarde la position originale
            Quaternion originalRotation = cam.transform.localRotation; // Sauvegarde la rotation originale

            while (Time.time < startTime + 30)
            {
                // Crée un zigzag en modifiant la position X et Y de manière sinusoïdale, variante avec le temps
                float drunkX = Mathf.Sin(Time.time * 2) * intensity;
                float drunkY = Mathf.Cos(Time.time * 2.5f) * intensity;
                cam.transform.localPosition = originalPosition + new Vector3(drunkX, drunkY, 0);

                // Ajoute une légère rotation basée sur l'amplitude sinusoïdale pour un effet de balancement
                float rotationZ = Mathf.Sin(Time.time) * rotationIntensity;
                cam.transform.localRotation = originalRotation * Quaternion.Euler(0, 0, rotationZ);

                yield return null; // Passe au prochain frame
            }
        
            // Réinitialise la position et la rotation de la caméra
            cam.transform.localPosition = originalPosition;
            cam.transform.localRotation = originalRotation;
            EventOnGoing = true;
        }

        IEnumerator Nothing()
        {
            yield return new WaitForSeconds(30);
            EventOnGoing = true;
        }
}
