using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Material laranja;
    public Material rosa;

    public float velocidade = 300f;
    public GameObject cuboPrefab;

    private static int playerIdAvailable;
    [SyncVar]private int playerId;

    private void Start()
    {
        Material material = null;

        if(isServer && isLocalPlayer)
        {
            material = laranja;
        }
        else if(isServer && !isLocalPlayer)
        {
            material = rosa;
        }
        else if(!isServer && isLocalPlayer)
        {
            material = rosa;
        }
        else if(!isServer && !isLocalPlayer)
        {
            material = laranja;
        }

        var meshRender = GetComponent<MeshRenderer>();
        meshRender.material = material;

        if (isServer)
        {
            if (isLocalPlayer)
            {
                playerIdAvailable = 0;
            }

            playerIdAvailable++;

            playerId = playerIdAvailable;
        }


    }

    private void Update()
    {

        if (isLocalPlayer)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * (v * Time.deltaTime));
            transform.Rotate(Vector3.up * h * velocidade * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.Space))
            {

                CmdSpawnarCubo();
            }
        }
    }

    [Command]
    private void CmdSpawnarCubo()
    {
        var cubo = Instantiate(cuboPrefab);

        cubo.transform.position = transform.position + (transform.forward * 2);
        cubo.transform.rotation = transform.rotation;


        cubo.GetComponent<cubo>().creatorId = playerId;

        NetworkServer.Spawn(cubo);
    }
}