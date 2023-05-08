using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] List<Car> carPrefabList;
    [SerializeField] float minCarSpawnInterval;
    [SerializeField] float maxCarSpawnInterval;  

    float timer;
    Vector3 carSpawnPosition;
    Quaternion carRotation;
    
    private void Start() {
        if (Random.value > 0.5f)
        {
            carRotation = Quaternion.Euler(0,-90 ,0);
            carSpawnPosition = new Vector3(
                (horizontalSize/2 + 4)*6,
                0.2f ,
                this.transform.position.z-1.6f);

        }
        else
        {
            carRotation = Quaternion.Euler(0,90,0);    
            carSpawnPosition = new Vector3(
                -(horizontalSize/2 + 4)*6,
                0.2f , 
                this.transform.position.z+1.6f);
        }   
        
    }

    private void Update() {
        var randomIndex = Random.Range(0,carPrefabList.Count);
        var carPreFab = carPrefabList[randomIndex];
        if (timer <= 0)
        {

            timer = Random.Range(
                minCarSpawnInterval,
                maxCarSpawnInterval);
            //spawn
            var car = Instantiate(
                carPreFab,
                carSpawnPosition,
                carRotation);
            
            car.SetupDistanceLimit((horizontalSize+7)*6);
            return;
        }
        timer  -= Time.deltaTime;
    }  
}
