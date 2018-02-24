using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblino : Astro {

    private WallController walls;

    protected override void FixedFrame() {
        
    }

    protected override void Frame() {
        
    }

    protected override void Init() {
        GameObject o = new GameObject();
        o.name = "WallController";
        walls = o.AddComponent<WallController>();
    }

    protected override void Dispose() {
        if(walls != null)
            Destroy(walls.gameObject);
    }
}
