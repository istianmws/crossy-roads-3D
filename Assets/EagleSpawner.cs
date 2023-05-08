using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] Eagle eagle;
    [SerializeField] Duck duck;
    [SerializeField] float initialTimer;

    float timer; 
    // Start is called before the first frame update
    void Start()
    {
        timer = initialTimer;
        eagle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<=0 && eagle.gameObject.activeInHierarchy == false)
        {
            eagle.gameObject.SetActive(true);
            eagle.transform.position = duck.transform.position + new Vector3(0,0,-15*6);
            duck.SetMoveable(false);
        }
        timer -= Time.deltaTime;
    }
    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
