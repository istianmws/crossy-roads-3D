using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    
    [SerializeField, Range(0,25)] float speed=20;
    // [SerializeField] float initialEagleTimer;
    // float timer;
    private void Update()
    {
        transform.Translate(Vector3.forward*6*speed*Time.deltaTime);
    }
    
}
