using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace TriggerSystem
{
    /// <summary>
    /// Singleton for collision check test.
    /// </summary>
    [AddComponentMenu("Trigger System/Trigger Simulation")]
    public class TriggerSimulation : MonoSingleton<TriggerSimulation>
    {
        private void FixedUpdate()
        {
            Simulate();
        }

        private void Simulate()
        {
            NativeList<SphereSphereJobData> sphereJobDatas = new NativeList<SphereSphereJobData>(Allocator.TempJob);
            NativeList<AABBAABBJobData> aabbJobDatas = new NativeList<AABBAABBJobData>(Allocator.TempJob);

            for (int senderIndex = 0; senderIndex < TriggerBaker.Instance.Triggers.Count; senderIndex++)
            {
                for (int receiverIndex = 0; receiverIndex < TriggerBaker.Instance.Triggers.Count; receiverIndex++)
                {
                    if (senderIndex == receiverIndex) continue;

                    var senderTrigger = TriggerBaker.Instance.Triggers[senderIndex];
                    var receiverTrigger = TriggerBaker.Instance.Triggers[receiverIndex];
                    var senderShape = senderTrigger.Shape;
                    var receiverShape = receiverTrigger.Shape;

                    // Check collision for sphere/sphere triggers.
                    if (senderShape == ShapeType.Sphere && receiverShape == ShapeType.Sphere)
                    {
                        var senderSphere = (SphereTrigger) senderTrigger;
                        var receiverSphere = (SphereTrigger) receiverTrigger;

                        sphereJobDatas.Add(
                            new SphereSphereJobData
                            {
                                Indexes = new IndexData
                                {
                                    SenderIndex = senderIndex,
                                    ReceiverIndex = receiverIndex
                                },
                                SenderPosition = senderSphere.transform.position,
                                SenderCenter = senderSphere.Data.Center,
                                SenderRadious = senderSphere.Data.Radius,
                                ReceiverPosition = receiverSphere.transform.position,
                                ReceiverCenter = receiverSphere.Data.Center,
                                ReceiverRadious = receiverSphere.Data.Radius,
                            });
                    }
                    // Check collision for box/box triggers.
                    else if (senderShape == ShapeType.Box && receiverShape == ShapeType.Box)
                    {
                        var senderBox = (BoxTrigger) senderTrigger;
                        var receiverBox = (BoxTrigger) receiverTrigger;

                        aabbJobDatas.Add(
                            new AABBAABBJobData
                            {
                                Indexes = new IndexData
                                {
                                    SenderIndex = senderIndex,
                                    ReceiverIndex = receiverIndex
                                },
                                senderMin = new float3(senderBox.transform.position + senderBox.Data.BoxBounds.min),
                                senderMax = new float3(senderBox.transform.position + senderBox.Data.BoxBounds.max),
                                receiverMin = new float3(receiverBox.transform.position + receiverBox.Data.BoxBounds.min),
                                receiverMax = new float3(receiverBox.transform.position + receiverBox.Data.BoxBounds.max)
                            });

                        // if (TriggerTestHelper.CheckAABBAABB(
                        //         new float3(senderBox.transform.position + senderBox.Data.BoxBounds.min),
                        //         new float3(senderBox.transform.position + senderBox.Data.BoxBounds.max),
                        //         new float3(receiverBox.transform.position + receiverBox.Data.BoxBounds.min),
                        //         new float3(receiverBox.transform.position + receiverBox.Data.BoxBounds.max)))
                        // {
                        //     senderTrigger.InvokeStayed(receiverTrigger);
                        // }
                    }
                    // Check collision for sphere/box triggers.
                    else if (senderShape == ShapeType.Sphere && receiverShape == ShapeType.Box)
                    {
                    }
                    // Check collision for box/sphere triggers.
                    else
                    {
                    }
                }
            }

            NativeArray<bool> sphereSphereResults = new NativeArray<bool>(sphereJobDatas.Length, Allocator.TempJob);

            var sphereSphereJob = new CheckSphereSphereJob
            {
                JobDatas = sphereJobDatas,
                Result = sphereSphereResults
            };

            JobHandle sphereSphereHandle = sphereSphereJob.Schedule(sphereJobDatas.Length, 1);

            sphereSphereHandle.Complete();

            for (int i = 0; i < sphereSphereResults.Length; i++)
            {
                if (sphereSphereResults[i])
                {
                    var sender = TriggerBaker.Instance.Triggers[sphereJobDatas[i].Indexes.SenderIndex];
                    var receiver = TriggerBaker.Instance.Triggers[sphereJobDatas[i].Indexes.ReceiverIndex];
                    sender.InvokeStayed(receiver);
                }
            }

            NativeArray<bool> aabbaabbResults = new NativeArray<bool>(aabbJobDatas.Length, Allocator.TempJob);

            var aabbaabbJob = new CheckAABBAABBJob
            {
                JobDatas = aabbJobDatas,
                Result = aabbaabbResults
            };
            
            JobHandle aabbaabbHandle = aabbaabbJob.Schedule(aabbJobDatas.Length, 1);
            
            aabbaabbHandle.Complete();

            for (int i = 0; i < aabbaabbResults.Length; i++)
            {
                if (aabbaabbResults[i])
                {
                    var sender = TriggerBaker.Instance.Triggers[aabbJobDatas[i].Indexes.SenderIndex];
                    var receiver = TriggerBaker.Instance.Triggers[aabbJobDatas[i].Indexes.ReceiverIndex];
                    sender.InvokeStayed(receiver);
                }
            }

            sphereJobDatas.Dispose();
            sphereSphereResults.Dispose();

            aabbJobDatas.Dispose();
            aabbaabbResults.Dispose();
        }
    }
}