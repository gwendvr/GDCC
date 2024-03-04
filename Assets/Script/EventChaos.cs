using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChaos : MonoBehaviour
{
    public Light[] LightMap;
    public GameObject[] Bot, Player;

    private bool EventOnGoing = false;
    private int EventId = -1;

    // Update is called once per frame
    void Update()
    {
        if (!EventOnGoing)
        {
            EventId = Random.Range(0, 2);
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
}
