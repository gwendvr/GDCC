using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayTaskPoint : MonoBehaviour
{
    public float timeToStay;
    public float timeReset;
    public float ultiGain;
    // Start is called before the first frame update

    private void Start()
    {
        timeReset = timeToStay;
    }
}
