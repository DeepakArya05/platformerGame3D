using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollecter : MonoBehaviour
{
    public float coinSpeed;
    public float rotateSpeed;
    public float coinHeight;

    private Vector3 startPos;
    private Vector3 targetPos;

    private void Awake()
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(0, coinHeight, 0);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, coinSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime,Space.World);
        
        if(transform.position==targetPos)
        {
            if (targetPos == startPos)
            {
                targetPos = startPos + new Vector3(0, coinHeight, 0);
            }
            else if (targetPos == startPos + new Vector3(0, coinHeight, 0))
            {
                targetPos = startPos;
            }
        }

    }

}
