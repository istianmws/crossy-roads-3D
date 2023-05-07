using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    // [SerializeField] List<Terrain> terrainPrefabList;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialGrassCount =  5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -7;
    [SerializeField] int forwardViewDistance = 15;
    Dictionary<int,Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;
    public UnityEvent<int,int> OnUpdateTerrainLimit;
    private void Start()
    {
        

        //terrain backward
        for (int zPos=backViewDistance; zPos<initialGrassCount;zPos++)
        {

            var terrain =Instantiate(terrainList[0]);
            terrain.transform.localPosition = new Vector3(0,0,-zPos*6);
            if (terrain is Grass grass)
            {
                grass.SetTreePercentage(zPos < -2 ? 1 : 0);
                
            }

            terrain.Generate(horizontalSize);

            activeTerrainDict[zPos]=terrain;
        }

        //terrain forward
        for (int zPos=initialGrassCount; zPos<forwardViewDistance;zPos++)
        {
            var terrain = SpawnRandomTerrain(zPos);  

        }
        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance+backViewDistance);
        // SpawnRandomTerrain(0);
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        int randomIndex;
        // Terrain terrain = null;
        for (int z = -1; z >= -3; z--)
        {
           var checkPos = (zPos+z);
           if (terrainCheck == null)
           {
                terrainCheck = activeTerrainDict[checkPos];
                continue;         
           }
           else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
           {
                randomIndex = Random.Range(0,terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos); 
           }
           else
           {
                continue;
           }
        }

        var candidateTerrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if (terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);
        return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }
    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0,0,zPos*-6);
        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
        return terrain;
    }
    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if(targetPosition.z < -travelDistance)
        {
            travelDistance = -Mathf.CeilToInt(targetPosition.z); 
            UpdateTerrain();
        }
    }
    public void UpdateTerrain()
    {
        var destroyPos = travelDistance/6 - 1 + backViewDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);

        var spawnPos = travelDistance/6 -1 + forwardViewDistance;
        SpawnRandomTerrain(spawnPos);

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance/6+backViewDistance);
    }
}
