using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Range(0,1)] float speed;
    private void Move()
    {
        transform.Translate(
            Vector3.forward * Time.deltaTime * speed);
    }
}
