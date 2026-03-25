/*using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GroundGuide : MonoBehaviour
{
    public NPC npc;
    public Transform startPoint;
    public Transform goalPoint;
    public float scrollSpeed = 1f;
    public float baseWidth = 0.2f;

    private LineRenderer lineRenderer;
    //private Vector3[] positions = new Vector3[2];
    private Vector3[] positions;
    private Vector3 lastStartPos, lastGoalPos;
    private Material runtimeMaterial;
    private Texture mainTexture;

    public float lineHeight = 1f;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = 2;
        lineRenderer.textureMode = LineTextureMode.DistributePerSegment;
        lineRenderer.useWorldSpace = true;

        runtimeMaterial = new Material(lineRenderer.material);
        lineRenderer.material = runtimeMaterial;
        //runtimeMaterial.renderQueue = 4000;

        mainTexture = runtimeMaterial.mainTexture;

        UpdateLine(force: true);
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        runtimeMaterial.mainTextureOffset = new Vector2(-offset, 0);

        if (startPoint.position != lastStartPos || goalPoint.position != lastGoalPos)
        {
            UpdateLine();
        }
    }

    void UpdateLine(bool force = false)
    {

        *//*if (npc == null) return;

        Debug.Log("distance npc = " + Vector3.Distance(npc.transform.position, MapController.Instance.Buildings[BuildingType.Base].transform.position));
        float distanceNPC = Vector3.Distance(npc.transform.position, MapController.Instance.Buildings[BuildingType.Base].transform.position);
        if (distanceNPC < 9f)
        {
            float scrollSpeed = 0.5f;
            float offset = Time.time * scrollSpeed;
            runtimeMaterial.mainTextureOffset = new Vector2(-offset, 0f);

            if (goalPoint == null) return;

            lineRenderer.positionCount = 2;
            lineRenderer.textureMode = LineTextureMode.DistributePerSegment;
            lineRenderer.textureScale = new Vector2(0.6f, 1);

            //lineRenderer.textureScale = Vector2.one;

            Vector3 start = npc.transform.position;
            Vector3 end = goalPoint.position;

            start.y = transform.position.y;
            end.y = transform.position.y;

            if (!force && start == lastStartPos && end == lastGoalPos)
            {
                return;
            }
            baseWidth = 1f;
            lineRenderer.startWidth = baseWidth;
            lineRenderer.endWidth = baseWidth;

            positions[0] = start;
            positions[1] = end;
            lineRenderer.SetPositions(positions);

            float distance = Vector3.Distance(start, end);

            runtimeMaterial.mainTextureScale = new Vector2(distance / baseWidth, 1);



            lastStartPos = start;
            lastGoalPos = end;
            return;
        }
*//*
        if (npc == null) return;

        Vector3[] v = npc.DrawPath(MapController.Instance.Buildings[BuildingType.Base].transform);
        positions = SmoothPath(v, 0.5f).ToArray();
        if (positions == null) return;

        *//*float scrollSpeedd = 1f;
        float offsett = Time.time * scrollSpeedd;
        lineRenderer.textureMode = LineTextureMode.DistributePerSegment;
        lineRenderer.textureScale = new Vector2(0.6f, 1);
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;*//*
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].y = lineHeight;
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

        float totalDistance = 0;

        for (int i = 0; i < positions.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(positions[i], positions[i + 1]);
        }

        runtimeMaterial.mainTextureScale = new Vector2(totalDistance / 0.5f, 1);

        *//*        float scrollSpeed = 2f;
                float offset = Time.time * scrollSpeed;
                runtimeMaterial.mainTextureOffset = new Vector2(-offset, 0f);*/

        /*if (goalPoint == null) return;

        Vector3 start = startPoint.position;
        Vector3 end = goalPoint.position;

        start.y = transform.position.y;
        end.y = transform.position.y;

        if (!force && start == lastStartPos && end == lastGoalPos)
        {
            return;
        }

        lineRenderer.startWidth = baseWidth;
        lineRenderer.endWidth = baseWidth;

        positions[0] = start;
        positions[1] = end;
        lineRenderer.SetPositions(positions);

        float distance = Vector3.Distance(start, end);

        runtimeMaterial.mainTextureScale = new Vector2(distance / baseWidth, 1);



        lastStartPos = start;
        lastGoalPos = end;*//*
    }

    public static List<Vector3> SmoothPath(Vector3[] corners, float spacing)
    {
        List<Vector3> result = new List<Vector3>();
        if (corners == null || corners.Length < 2) return result;

        result.Add(corners[0]);

        for (int i = 0; i < corners.Length - 1; i++)
        {
            Vector3 start = corners[i];
            Vector3 end = corners[i + 1];
            float distance = Vector3.Distance(start, end);
            int steps = Mathf.Max(1, Mathf.FloorToInt(distance / spacing));

            for (int j = 1; j <= steps; j++)
            {
                Vector3 point = Vector3.Lerp(start, end, (float)j / steps);
                result.Add(point);
            }
        }

        return result;
    }
}
*/