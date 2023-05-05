using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    // [SerializeField] List<Terrain> terrainPrefabList;
    [SerializeField] Grass grassPreFab;
    [SerializeField] Road roadPreFab;

    [SerializeField] int initialGrassCount =  5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -5;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0,1)] float treeProbability;
    private void Start() {
        
        for (int zPos=backViewDistance; zPos<initialGrassCount;zPos++)
        {

            var grass =Instantiate(grassPreFab);
            grass.transform.localPosition = new Vector3(0,0,-zPos*6);
            
            grass.SetTreePercentage(zPos < -2 ? 1 : 0);

            grass.Generate(horizontalSize);
        }

        for (int zPos=initialGrassCount; zPos<forwardViewDistance;zPos++)
        {

            var terrain =Instantiate(roadPreFab);
            terrain.transform.localPosition = new Vector3(0,0,-zPos*6);
            
            // grass.SetTreePercentage(zPos < -2 ? 1 : 0);

            terrain.Generate(horizontalSize);
        }
    }
}
