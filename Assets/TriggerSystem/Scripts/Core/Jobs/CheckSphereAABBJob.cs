using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace TriggerSystem
{
    [BurstCompile]
    public struct CheckSphereAABBJob : IJobParallelFor
    {
        [Unity.Collections.ReadOnly]
        public NativeList<SphereAABBJobData> JobDatas;

        [WriteOnly]
        public NativeArray<bool> Result;

        public void Execute(int index)
        {
            Result[index] = CheckSphereAABB(
                JobDatas[index].SphereRadius,
                JobDatas[index].SphereCenter,
                JobDatas[index].BoxMin,
                JobDatas[index].BoxMax
            );
        }

        /// <summary>
        /// Checks collision between a sphere and a AABB box.
        /// </summary>
        /// <param name="sphereCenter"></param>
        /// <param name="boxMin"></param>
        /// <param name="boxMax"></param>
        /// <returns>Returns true when these shapes collides with each other.</returns>
        public static bool CheckSphereAABB(
            float sphereRadius,
            float3 sphereCenter,
            float3 boxMin,
            float3 boxMax)
        {
            float x = math.max(boxMin.x, math.min(sphereCenter.x, boxMax.x));
            float y = math.max(boxMin.y, math.min(sphereCenter.y, boxMax.y));
            float z = math.max(boxMin.z, math.min(sphereCenter.z, boxMax.z));

            //TODO: This can be improved by not calculating square root of arguments.
            float distance = math.distance(new float3(x, y, z), sphereCenter);

            bool overlapped = distance < sphereRadius;

            return overlapped;
        }
    }
}