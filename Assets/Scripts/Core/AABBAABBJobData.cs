using Unity.Mathematics;

namespace TriggerSystem
{
    public struct AABBAABBJobData
    {
        public IndexData Indexes;

        public float3 senderMin;
        public float3 senderMax;
        
        public float3 receiverMin;
        public float3 receiverMax;
    }
}