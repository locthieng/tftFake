using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public List<Transform> mapTransforms = new List<Transform>();
    public Transform mapMain;

    private void Start()
    {
        SetUp();    
    }

    public void SetUp()
    {
        //MoveToMapEnemy();
    }    

    private void MoveToMapEnemy()
    {
        int index = Random.Range(0, mapTransforms.Count); 
        CameraController.Instance.FollowTarget(mapTransforms[index]);
    }    
}
