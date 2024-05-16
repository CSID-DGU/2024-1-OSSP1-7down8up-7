using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public NavMeshSurface Surface2D;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        Surface2D = GetComponent<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
