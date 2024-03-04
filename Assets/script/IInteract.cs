using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    float ct { get; set; }
    bool available { get; set; }
    //BoxCollider2D triggerArea { get; }
    //void OnCollisionEnter2D(Collision2D collision);
    //void OnCollisionExit2D(Collision2D collision);
}
