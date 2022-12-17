using Unity.Mathematics;

namespace TriggerSystem
{
    public struct SphereSphereJobData
    {
        public IndexData Indexes;
        
        public float3 SenderPosition;
        public float3 SenderCenter;
        public float SenderRadious;

        public float3 ReceiverPosition;
        public float3 ReceiverCenter;
        public float ReceiverRadious;

        public struct IndexData
        {
            public int SenderIndex;
            public int ReceiverIndex;
        }
    }
}