using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePreFabList;
    [SerializeField, Range(0,1)] float treeProbability;

    public void SetTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(newProbability);        
    }
    public override void Generate(int size)
    {
        base.Generate(size);
        var limit = Mathf.FloorToInt((float)size/2);
        var treeCount = Mathf.FloorToInt((float)size*treeProbability);

       

        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }

        // Debug.Log(string.Join(",",emptyPosition));
        
        for (int i = 0; i<treeCount; i++)
        {
            // Debug.Log(i + string.Join(",",emptyPosition));
            var randomIndex = Random.Range(0,emptyPosition.Count);
            var pos = 6*emptyPosition[randomIndex];

            emptyPosition.RemoveAt(randomIndex);
            SpawnRandomTree(pos);
        }
        
        SpawnRandomTree(6*(-limit-1));
        SpawnRandomTree((limit+1)*6);
    }

    
    private void SpawnRandomTree(int pos)
    {
        var randomIndex = Random.Range(0,treePreFabList.Count);
        var prefab = treePreFabList[randomIndex];

        var tree = Instantiate(
            prefab, 
            new Vector3(pos,0,this.transform.position.z),
            Quaternion.identity,
            transform); 
    }
}
