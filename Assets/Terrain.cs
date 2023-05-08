using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Terrain : MonoBehaviour {
    
[SerializeField] GameObject tilePrefab;
protected int horizontalSize;

    public virtual void Generate(int size)
    {
        horizontalSize = size;
        if (size == 0)
            return;

        if ((float)size%2 ==0)
            size-=1;

        int limit = Mathf.FloorToInt((float)size/2);
        for (int i=-limit; i<=limit;i++)
        {
            SpawnTile(i);
        }
        // create empty array of gameobjects with 6 elements
        GameObject[] boundaryTiles = new GameObject[8];

        // spawn left boundary tiles
        for (int i = 1; i <= 4; i++)
        {
            boundaryTiles[i] = SpawnTile(-limit - i);
            DarkenObject(boundaryTiles[i]);
            boundaryTiles[i+3] = SpawnTile(limit + i);
            DarkenObject(boundaryTiles[i+3]);
        }

        // // spawn right boundary tiles
        // for (int i = 1; i <= 3; i++)
        // {
        // }

        // darken the left and right boundary tiles
        // foreach (GameObject tile in boundaryTiles)
        // {
        //     DarkenObject(tile);
        // }

        // var leftBoundaryTile = SpawnTile(-limit-1);
        // var leftBoundaryTile2 = SpawnTile(-limit-2);
        // var leftBoundaryTile3 = SpawnTile(-limit-3);
        // var rightBoundaryTile = SpawnTile(limit+1);
        // var rightBoundaryTile2 = SpawnTile(limit+2);
        // var rightBoundaryTile3 = SpawnTile(limit+3);
        
        // DarkenObject(leftBoundaryTile);
        // DarkenObject(leftBoundaryTile2);
        // DarkenObject(leftBoundaryTile3);
        // DarkenObject(rightBoundaryTile); 
        // DarkenObject(rightBoundaryTile2); 
        // DarkenObject(rightBoundaryTile3);           
    }

    private GameObject SpawnTile(int xPos)
    {
        var go = Instantiate(tilePrefab,transform);
        go.transform.localPosition = new Vector3(xPos*6,0,0);
        return go;
    }

    private void DarkenObject(GameObject go)
    {
        var renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        foreach (var rend in renderers)
        {
            rend.material.color *= Color.grey;
        }
    }
}