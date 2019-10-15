using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cubo : NetworkBehaviour
{

    [SyncVar] public int creatorId;

    public Material laranja;
    public Material rosa;


    // Start is called before the first frame update
    void Start()
    {
        Material material = creatorId == 1 ? laranja : rosa;

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
