using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesesPathfindingAgent : MonoBehaviour
{
    public CheesesSimplePathfinder.PathRequest request;

    public float maximumNodeDistance = 50;
    public float terrainSamples = 10;

    public float oceanMovementSpeedPenalty = 1;
    public float landMovementSpeedPenalty = 1;

    public float searchWidth = 1;
    public float searchHeight = 1;

    public bool visualisePath = true;
    public GameObject pathVisObj;

    public void MoveTo(Vector3D position)
    {
        request = new CheesesSimplePathfinder.PathRequest(this, VTMapManager.WorldToGlobalPoint(transform.position), position,
            maximumNodeDistance, terrainSamples,
            oceanMovementSpeedPenalty, landMovementSpeedPenalty,
            searchWidth, searchHeight);
        CheesesSimplePathfinder.instance.RequestPath(request);
    }

    public void OnPathComplete()
    {
        if (visualisePath)
        {
            SpawnVisualiserCubes();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (request == null || request.path == null || request.path.Count == 0)
            return;

        Gizmos.color = Color.red;
        for (int i = 1; i < request.path.Count; i++)
        {
            Gizmos.DrawLine(request.path[i - 1].toVector3, request.path[i].toVector3);
        }
    }


    private void SpawnVisualiserCubes()
    {
        Debug.Log($"Spawning path visualisation!");

        if (pathVisObj != null)
        {
            Destroy(pathVisObj);
        }

        //pathVisObj = new GameObject();
        pathVisObj = request.followPath.gameObject;
        pathVisObj.AddComponent<FloatingOriginTransform>();

        for (int i = 0; i < request.path.Count - 1; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());

            Vector3 pos1 = VTMapManager.GlobalToWorldPoint(request.path[i]);
            Vector3 pos2 = VTMapManager.GlobalToWorldPoint(request.path[i + 1]);;

            Vector3 center = (pos1 + pos2) * 0.5f;
            Quaternion rot = Quaternion.LookRotation(pos2 - pos1);

            cube.transform.position = center;
            cube.transform.rotation = rot;
            cube.transform.localScale = new Vector3(1, 1, (pos2 - pos1).magnitude);
            cube.transform.parent = pathVisObj.transform;
        }
    }

    private void OnDestroy()
    {
        if (pathVisObj != null)
        {
            Destroy(pathVisObj);
        }

        if (request != null && request.followPath != null)
        {
            Destroy(request.followPath.gameObject);
        }
    }
}
