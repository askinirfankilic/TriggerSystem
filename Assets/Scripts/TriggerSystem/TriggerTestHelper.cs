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
        public static bool CheckSphereSphere(float3 worldPos1, float3 center1, float radius1, float3 worldPos2,
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
    }
}