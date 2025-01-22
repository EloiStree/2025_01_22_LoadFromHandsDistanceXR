using System.Collections.Generic;
using UnityEngine;

public class HandLoaderMono_ : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class SceneToLoadIfFlatHandDetected : WhatToUseIfFlatHandLoadDetected<string>
{
}

[System.Serializable]
public class PrefabToSpawnIfFlatHandDetected : WhatToUseIfFlatHandLoadDetected<GameObject>
{
}


