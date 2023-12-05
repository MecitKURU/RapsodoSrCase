using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static List<Vector3> _positions = new List<Vector3>();
    public static Vector3 GetRandomPosition(Terrain terrain) // It returns random point within a certain area to create random objects.
    {
        float offset = 30;

        float minX = -terrain.terrainData.size.x / 2 + offset;
        float maxX = terrain.terrainData.size.x / 2 - offset;

        float minZ = -terrain.terrainData.size.z / 2 + offset;
        float maxZ = terrain.terrainData.size.z / 2 - offset;

        Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        float posY = terrain.SampleHeight(position);
        position.y = posY;

        while (!IsFarEnoughFromOtherPoints(position, 3f))
        {
            position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            posY = terrain.SampleHeight(position);
            position.y = posY;
        }

        _positions.Add(position);
        return position;
    }
    private static bool IsFarEnoughFromOtherPoints(Vector3 point, float minDistance)
    {
        foreach (var existingPoint in _positions)
        {
            if (Vector3.Distance(existingPoint, point) < minDistance)
            {
                return false; // returns false if not far enough from others
            }
        }
        return true; // returns true if it is far enough away from the others
    }
    public static bool IsVisible(Vector3 position, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera); // This function takes the given camera's view frustum and returns six planes that form it

        foreach (var plane in planes)
        {
            Vector3 newPosition = new Vector3(position.x, plane.ClosestPointOnPlane(position).y, position.z); // just to comparison with x and z coordinates
            if (plane.GetDistanceToPoint(newPosition) < -3) // I defined an offset of 3 units because I wanted the object to disappear when it was 3 units away from the camera.
            {
                return false;
            }
        }
        return true;
    }

}
