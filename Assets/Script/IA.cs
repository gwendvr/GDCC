using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent ai;
    public Animator aiAnim;
    int randNum, lastRandNum = -1; // Ajoute lastRandNum pour garder en mémoire la dernière destination.
    public Transform[] RandDest;
    public bool walking, idle;
    Vector3 dest;

    void Start()
    {
        walking = true;
        SelectNewDestination(); // Utilise une méthode pour sélectionner une nouvelle destination.
        aiAnim.SetTrigger("walk");
    }
    void Update()
    {
        if (walking == true)
        {
            ai.destination = dest;
            ai.speed = 3;
        }
        if (idle == true)
        {
            ai.speed = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("destination"))
        {
            idle = true;
            walking = false;
            aiAnim.ResetTrigger("walk");
            aiAnim.SetTrigger("idle");
            StartCoroutine("nextDest");
        }
    }
    IEnumerator nextDest()
    {
        yield return new WaitForSeconds(Random.Range(1,7));
        idle = false;
        walking = true;
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");
        SelectNewDestination(); // Réutilise la méthode pour s'assurer de ne pas répéter la destination.
    }

    void SelectNewDestination()
    {
        do
        {
            randNum = Random.Range(0, RandDest.Length);
        } while (randNum == lastRandNum); // Continue de choisir aléatoirement une destination jusqu'à ce qu'elle soit différente de la dernière.
        
        lastRandNum = randNum; // Mets à jour lastRandNum à la fin de la méthode.
        dest = RandDest[randNum].position;
    }
}
