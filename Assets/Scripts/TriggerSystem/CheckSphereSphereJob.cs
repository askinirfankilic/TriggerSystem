using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace TriggerSystem
{
    [BurstCompile]
    public struct CheckSphereSphereJob : IJobParallelFor
    {
        [Unity.Collections.ReadOnly]
        public NativeList<SphereSphereJobData> JobDatas;
        [WriteOnly]
        public NativeArray<bool> Result;

        public void Execute(int index)
        {
            Result[index] = CheckSphereSphere(
                JobDatas[index].SenderPosition,
                JobDatas[index].SenderCenter,
                JobDatas[index].SenderRadious,
                JobDatas[index].ReceiverPosition,
                JobDatas[index].ReceiverCenter,
                JobDatas[index].ReceiverRadious
            );
        }
        
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
        public bool CheckSphereSphere(
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
    }
}