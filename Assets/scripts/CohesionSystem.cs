using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CohesionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach ((ref Translation boidTrans, ref Rotation boidRot, ref BoidComponent boidComponent) => 
        {
            float3 boidPos = boidTrans.Value;
            float coheDis = boidComponent.cohesionDistance;
            float avoidDis = boidComponent.avoidanceDistance;

            List <float3> cohesionNeighbours = new List<float3> ();
            float3 m_cohesionDir = float3.zero;
            float3 m_avoidDir = float3.zero;
            float3 m_allignDir = float3.zero;
            float avgSpeed = 0.01f;

            int neighbourCount = 0;

            Entities.WithAll <BoidComponent> ().ForEach ((ref Translation neighbourTranslation, ref BoidComponent neighbourBoid, ref LocalToWorld neighLocalToWorld) => {
                float neighDistance = math.distance (boidPos, neighbourTranslation.Value);
                if (neighDistance <= coheDis) {
                    neighbourCount++;
                    m_cohesionDir += neighbourTranslation.Value;
                    m_allignDir += neighLocalToWorld.Forward;

                    if (neighDistance <= avoidDis) {
                        m_avoidDir += boidPos - neighbourTranslation.Value;
                    }

                    avgSpeed += neighbourBoid.speed;
                }
            });
            if (neighbourCount > 0) {

                m_cohesionDir /= neighbourCount;
                m_allignDir /= neighbourCount;
                
                boidComponent.speed = avgSpeed / neighbourCount; 
                boidComponent.speed = math.clamp (boidComponent.speed, 0, boidComponent.maxSpeed);
                
                boidComponent.cohesionDir = m_cohesionDir;
                boidComponent.allignDir = m_allignDir;

            }

            
        });
    }
}
