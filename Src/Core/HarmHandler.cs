using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class HarmHandler : MonoBehaviour
{

    Weapon weapon;
    void Start()
    {
        weapon = GetComponent<PlayerController>().weapon;
    }
    /*void Attack(){
        
    }*/
}
