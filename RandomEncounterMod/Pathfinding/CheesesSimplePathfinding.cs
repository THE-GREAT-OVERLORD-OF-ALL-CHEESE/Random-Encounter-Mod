using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesesSimplePathfinder : MonoBehaviour
{
    public enum RequestStatus
    {
        Waiting,
        Generated
    }

    public class PathRequest
    {
        public PathRequest(CheesesPathfindingAgent agent, Vector3D startPos, Vector3D endPos,
            float maximumNodeDistance,
            float terrainSamples,
            float oceanMovementSpeedPenalty,
            float landMovementSpeedPenalty,
            float searchWidth,
            float searchHeight)
        {
            this.agent = agent;
            this.startPos = startPos;
            this.endPos = endPos;

            this.maximumNodeDistance = maximumNodeDistance;
            this.terrainSamples = terrainSamples;

            this.oceanMovementSpeedPenalty = oceanMovementSpeedPenalty;
            this.landMovementSpeedPenalty = landMovementSpeedPenalty;

            this.searchWidth = searchWidth;
            this.searchHeight = searchHeight;
        }

        public CheesesPathfindingAgent agent;
        public RequestStatus status = RequestStatus.Waiting;

        public Vector3D startPos;
        public Vector3D endPos;

        public List<Vector3D> path;
        public FollowPath followPath;

        public float maximumNodeDistance = 50;
        public float terrainSamples = 10;

        public float oceanMovementSpeedPenalty = 1;
        public float landMovementSpeedPenalty = 1;

        public float searchWidth = 1;
        public float searchHeight = 1;
    }

    public static CheesesSimplePathfinder instance;

    private List<PathRequest> requests;
    public Coroutine pathfindingCoroutine;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        requests = new List<PathRequest>();
    }

    public void RequestPath(PathRequest newRequest)
    {
        requests.Add(newRequest);
    }

    private void FixedUpdate()
    {
        if (pathfindingCoroutine == null && requests.Count > 0)
        {
            PathRequest currentRequest = requests[0];
            requests.RemoveAt(0);

            Debug.Log($"Theres a path in the queue, completing path request for {currentRequest.agent.gameObject.name}");

            pathfindingCoroutine = StartCoroutine(FindPath(currentRequest));
        }
    }
    private IEnumerator FindPath(PathRequest request)
    {
        List<Vector3D> path = new List<Vector3D>();
        yield return StartCoroutine(FindPath(request, request.startPos, request.endPos, path, 0));

        Debug.Log($"Rough path found optimising path.");

        request.path = new List<Vector3D>();
        request.path.Add(request.startPos);
        request.path.AddRange(path);
        request.searchHeight = 0;

        for (int x = 0; x < 3; x++)
        {
            for (int i = 1; i < path.Count - 1; i++)
            {
                yield return new WaitForFixedUpdate();

                Vector3D startPos = path[i - 1];
                Vector3D endPos = path[i + 1];

                Vector3D average = (startPos + endPos) / 2f;
                float radius = (float)((startPos - endPos).magnitude / 2f);

                path[i] = PickBestMidpoint(request, startPos, endPos, average, radius, Color.cyan);
            }

            Debug.Log($"Path optimised, step {x+1}/3");
        }

        request.followPath = VectorListToFollowPath(path);
        Debug.Log($"Path converted to vtol follow path");

        request.status = RequestStatus.Generated;
        request.agent.OnPathComplete();

        Debug.Log($"Path completed!");

        pathfindingCoroutine = null;
    }

    private IEnumerator FindPath(PathRequest request, Vector3D startPos, Vector3D endPos, List<Vector3D> path, int depth)
    {
        if ((startPos - endPos).magnitude < request.maximumNodeDistance || depth >= 10)
        {
            path.Add(endPos);
        }
        else
        {

            Vector3D average = (startPos + endPos) / 2f;
            float radius = (float)((startPos - endPos).magnitude / 2f);

            Vector3D midPoint = PickBestMidpoint(request, startPos, endPos, average, radius, Color.black);

            yield return new WaitForFixedUpdate();

            List<Vector3D> pathStart = new List<Vector3D>();
            List<Vector3D> pathEnd = new List<Vector3D>();

            yield return StartCoroutine(FindPath(request, startPos, midPoint, pathStart, depth + 1));
            yield return StartCoroutine(FindPath(request, midPoint, endPos, pathEnd, depth + 1));

            path.InsertRange(0, pathEnd);
            path.InsertRange(0, pathStart);
        }
    }

    private Vector3D PickBestMidpoint(PathRequest request, Vector3D startPos, Vector3D endPos, Vector3D center, float radius, Color color)
    {
        Vector3D bestPoint = center;
        float bestPointCost = Mathf.Infinity;

        Vector3 fwd = (endPos - startPos).normalized.toVector3;
        Vector3 right = Vector3.Cross(fwd, Vector3.up).normalized;

        for (int i = 0; i < request.terrainSamples; i++)
        {
            Vector3 randomVector = Random.insideUnitCircle;
            randomVector = randomVector.y * request.searchHeight * fwd + randomVector.x * request.searchWidth * right;

            Vector3D airPoint = center + randomVector * radius;

            RaycastHit hit;
            Vector3D groundPos = airPoint;
            float asl = 0;
            float gradient = 0;

            if (Physics.Raycast(VTMapManager.GlobalToWorldPoint(airPoint) + Vector3.up * 1000, Vector3.down, out hit, 2000))
            {
                groundPos = VTMapManager.WorldToGlobalPoint(hit.point);
                if (groundPos.y < 0)
                {
                    groundPos.y = 0;
                }
                asl = (float)(VTMapManager.WorldToGlobalPoint(hit.point).y - groundPos.y);
                gradient = Mathf.Tan(Vector3.Angle(Vector3.up, hit.normal) * Mathf.Deg2Rad);
            }

            //Debug.DrawRay(groundPos, Vector3.up * 10, color, 1f);

            float currentCost = CalculatePathCost(request, startPos, groundPos, asl, gradient) + CalculatePathCost(request, groundPos, endPos, asl, gradient);

            if (currentCost < bestPointCost)
            {
                bestPointCost = currentCost;
                bestPoint = groundPos;
            }
        }

        return bestPoint;
    }

    private float CalculatePathCost(PathRequest request, Vector3D startPos, Vector3D endPos, float altitude, float gradient)
    {
        Vector3D offset = startPos - endPos;
        float distance = (float)offset.magnitude;

        float traverseSpeed = 1;

        float pathGradient = Mathf.Abs((float)offset.y) / distance;
        float totalGradient = (gradient + pathGradient) * 0.5f;

        if (startPos.y < 1f || endPos.y < 1f)
        {
            traverseSpeed *= 1f / request.oceanMovementSpeedPenalty;
        }
        if (altitude > -25f)
        {
            traverseSpeed *= 1f / request.landMovementSpeedPenalty;
        }

        traverseSpeed *= 1f / GradientToSpeedPenalty(totalGradient);

        return distance / traverseSpeed;
    }

    private float GradientToSpeedPenalty(float gradient)
    {
        return 1 + Mathf.Pow(gradient * 2.5f, 2f);
    }

    private FollowPath VectorListToFollowPath(List<Vector3D> path)
    {
        GameObject pathObj = new GameObject("Path");
        pathObj.AddComponent<FloatingOriginTransform>();

        FollowPath followPath = gameObject.AddComponent<FollowPath>();
        followPath.uniformlyPartition = true;

        List<Transform> list = new List<Transform>();
        foreach (Vector3D point in path)
        {
            GameObject pointObj = new GameObject("PathPoint");
            pointObj.transform.position = VTMapManager.GlobalToWorldPoint(point);
            pointObj.transform.parent = pathObj.transform;
            list.Add(pointObj.transform);
        }
        followPath.pointTransforms = list.ToArray();
        
        followPath.loop = false;
        followPath.SetPathMode(Curve3D.PathModes.Linear);
        followPath.SetupCurve();

        return followPath;
    }
}
