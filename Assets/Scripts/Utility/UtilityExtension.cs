using UnityEngine;

namespace ZRUtility
{
    public static class UtilityExtension
    {
        public static bool GetPositionAround(this Transform centerObject, float minRadius,float maxRadius, LayerMask groundLayer, LayerMask obstacleLayer, float minObstacleDistance, out Vector3 validPoint)
        {
            Vector3 center = centerObject.position;
            validPoint = Vector3.zero;

            for (int i = 0; i < 30; i++)
            {
                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                float distance = Random.Range(minRadius, maxRadius);

                Vector3 randomPos = new Vector3(
                    center.x + distance * Mathf.Cos(angle),
                    center.y + 10f,
                    center.z + distance * Mathf.Sin(angle)
                );

                if (Physics.CheckSphere(randomPos, minObstacleDistance, obstacleLayer))
                    continue;

                if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, 20f, groundLayer))
                {
                    validPoint = hit.point;
                    return true;
                }
            }

            return false;
        }
    }
}
