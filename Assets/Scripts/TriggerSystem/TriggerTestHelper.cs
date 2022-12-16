using Unity.Mathematics;

namespace TriggerSystem
{
    public static class TriggerTestHelper
    {
        /// <summary>
        /// Checks collision between two spherical object.
        /// </summary>
        /// <param name="worldPos1"></param>
        /// <param name="center1"></param>
        /// <param name="radius1"></param>
        /// <param name="worldPos2"></param>
        /// <param name="center2"></param>
        /// <param name="radius2"></param>
        /// <returns>Returns true when sphere collides with other sphere</returns>
        public static bool CheckSphereSphere(
            float3 worldPos1,
            float3 center1,
            float radius1,
            float3 worldPos2,
            float3 center2,
            float radius2)
        {
            float3 senderWorldPosition = worldPos1 + center1;
            float3 receiverWorldPosition = worldPos2 + center2;

            float senderRadius = radius1;
            float receiverRadius = radius2;

            float distanceSquared = math.distancesq(senderWorldPosition, receiverWorldPosition);
            float totalRadiusSquared = math.pow(senderRadius + receiverRadius, 2);

            bool overlapped = distanceSquared < totalRadiusSquared;

            return overlapped;
        }

        /// <summary>
        /// Checks collision between two bounding box object.
        /// </summary>
        /// <param name="min1"></param>
        /// <param name="max1"></param>
        /// <param name="min2"></param>
        /// <param name="max2"></param>
        /// <returns>Returns true when box collides with other box</returns>
        public static bool CheckAABBAABB(
            float3 min1,
            float3 max1,
            float3 min2,
            float3 max2
        )
        {
            bool overlapped = min1.x <= max2.x &&
                              max1.x >= min2.x &&
                              min1.y <= max2.y &&
                              max1.y >= min2.y &&
                              min1.z <= max2.z &&
                              max1.z >= min2.z;

            return overlapped;
        }
    }
}