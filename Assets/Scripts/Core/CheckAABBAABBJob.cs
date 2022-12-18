using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace TriggerSystem
{
    [BurstCompile]
    public struct CheckAABBAABBJob : IJobParallelFor
    {
        [Unity.Collections.ReadOnly]
        public NativeList<AABBAABBJobData> JobDatas;
        
        [WriteOnly]
        public NativeArray<bool> Result;
        
        public void Execute(int index)
        {
            Result[index] = CheckAABBAABB(
                JobDatas[index].senderMin,
                JobDatas[index].senderMax,
                JobDatas[index].receiverMin,
                JobDatas[index].receiverMax
            );
        }
        
        /// <summary>
        /// Checks collision between two bounding box object.
        /// </summary>
        /// <param name="min1"></param>
        /// <param name="max1"></param>
        /// <param name="min2"></param>
        /// <param name="max2"></param>
        /// <returns>Returns true when box collides with other box</returns>
        public bool CheckAABBAABB(
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