using Unity.Mathematics;
using UnityEngine;

namespace TriggerSystem
{
    public class TriggerSimulation : MonoBehaviour
    {
        private void FixedUpdate()
        {
            //TODO: This search should be baked at initialization. After that another system may check if any trigger component deactivated in scene.
            var spheres = FindObjectsOfType<SphereTrigger>();
            if (spheres == null) return;

            Simulate(spheres);
        }

        private void Simulate(SphereTrigger[] spheres)
        {
            for (int senderIndex = 0; senderIndex < spheres.Length; senderIndex++)
            {
                for (int receiverIndex = 0; receiverIndex < spheres.Length; receiverIndex++)
                {
                    if (senderIndex == receiverIndex) continue;

                    if (TriggerTestHelper.CheckSphereSphere(
                            // Sender data assignment.
                            spheres[senderIndex].transform.position,
                            spheres[senderIndex].Data.Center,
                            spheres[senderIndex].Data.Radius,
                            // Receiver data assignment.
                            spheres[receiverIndex].transform.position,
                            spheres[receiverIndex].Data.Center,
                            spheres[receiverIndex].Data.Radius))
                    {
                        spheres[senderIndex].InvokeStayed(spheres[receiverIndex]);
                    }
                }
            }
        }
    }
}