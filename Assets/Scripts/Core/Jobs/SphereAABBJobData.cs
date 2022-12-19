using Unity.Mathematics;

namespace TriggerSystem
{
    public struct SphereAABBJobData
    {
        // False means AABB is sender.
        public bool IsSphereSender;
        
        public IndexData Indexes;

        public float SphereRadius;
        public float3 SphereCenter;

        public float3 BoxMin;
        public float3 BoxMax;
    }
}