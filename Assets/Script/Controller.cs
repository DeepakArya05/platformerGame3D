using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{
    Playercontroller playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        playercontroller = FindObjectOfType<Playercontroller>();
    }


    public void jump()
    {
        playercontroller.jump = true;
    }

}
