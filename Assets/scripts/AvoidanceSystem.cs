using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class AvoidanceSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach ((ref Translation boidTrans, ref Rotation boidRot, ref BoidComponent boidComponent) => 
        {
            float3 boidPos = boidTrans.Value;
            float avoidDis = boidComponent.avoidanceDistance;

            float3 m_avoidDir = float3.zero;

            Entities.WithAll <BoidComponent> ().ForEach ((ref Translation neighbourTranslation, ref BoidComponent neighbourBoid, ref LocalToWorld neighLocalToWorld) => {
                float neighDistance = math.distance (boidPos, neighbourTranslation.Value);
                
                if (neighDistance <= avoidDis) {
                        m_avoidDir += boidPos - neighbourTranslation.Value;
                }
            });

            boidComponent.avoidDir = m_avoidDir;

            
        });
    }
}
