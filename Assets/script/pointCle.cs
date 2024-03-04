using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointCle : MonoBehaviour,IInteract
{
    public float clearTime;
    public float ct { get; set; }
    public bool available { get; set; }

    void Start()
    {
        ct = clearTime;
        available = true;
    }

    void Update()
    {
        
    }
}
