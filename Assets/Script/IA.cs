using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        Walking,
        Sprinting,
        Sneaking
    }

    public NavMeshAgent ai;
    public Animator aiAnim;
    int randNum, lastRandNum = -1;
    public Transform[] RandDest;
    public float actionTime, walkSpeed, runSpeed, sneakSpeed;
    Vector3 dest;
    bool isJumping = false, isCrouching = false;
    float speed;
    public AIState currentState;

    [SerializeField] private float timeToChangeDestination; // Ajouté pour le changement aléatoire
    private float timeUntilChangeDestination;
    public float speedMultiplier = 1.5f; // Facteur multiplicatif pour augmenter la vitesse de déplacement

    void Start()
    {
        SelectNewDestination();
        StartCoroutine(RandomAction());
        timeUntilChangeDestination = timeToChangeDestination; // Initialisation du compte à rebours
    }

    void Update()
    {
        if (!isJumping && !isCrouching)
        {
            ai.destination = dest;

            switch (currentState)
            {
                case AIState.Idle:
                    speed = 0;
                    break;
                case AIState.Walking:
                    speed = walkSpeed;
                    break;
                case AIState.Sprinting:
                    speed = runSpeed;
                    break;
                case AIState.Sneaking:
                    speed = sneakSpeed; // Assigne une vitesse propre à l'état de sneak
                    break;
            }

            ai.speed = speed * speedMultiplier; // Modification pour utiliser speedMultiplier
            aiAnim.SetFloat("Speed", speed);

            timeUntilChangeDestination -= Time.deltaTime;
            if(timeUntilChangeDestination <= 0)
            {
                SelectNewDestination();
                timeUntilChangeDestination = timeToChangeDestination;
            }
        }
    }

    IEnumerator RandomAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(actionTime);
            int action = Random.Range(0, 5);

            switch (action)
            {
                case 0: // Idle
                    isJumping = isCrouching = false;
                    currentState = AIState.Idle;
                    break;
                case 1: // Marche
                    isJumping = isCrouching = false;
                    currentState = AIState.Walking;
                    break;
                case 2: // Sprint
                    isJumping = isCrouching = false;
                    currentState = AIState.Sprinting;
                    break;
                case 3: // Saut
                    isJumping = true;
                    aiAnim.SetBool("Jump", true);
                    yield return new WaitForSeconds(1);
                    aiAnim.SetBool("Jump", false);
                    isJumping = false;
                    break;
                case 4: // Se met accroupi (Sneak)
                    isCrouching = true;
                    aiAnim.SetBool("IsSneaking", true);
                    currentState = AIState.Sneaking;
                    yield return new WaitForSeconds(2);
                    aiAnim.SetBool("IsSneaking", false);
                    isCrouching = false;
                    break;
            }
        }
    }

    void SelectNewDestination()
    {
        do
        {
            randNum = Random.Range(0, RandDest.Length);
        } while (randNum == lastRandNum);
        lastRandNum = randNum;
        dest = RandDest[randNum].position;
    }
}
