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
            float3 cohesionDir = float3.zero;
            float3 avoidDir = float3.zero;
            float3 avgForward = float3.zero;
            float avgSpeed = 0.01f;

            int neighbourCount = 0;
            if (boidComponent.returning) {
                    boidRot.Value = Quaternion.Slerp (boidRot.Value, Quaternion.LookRotation (float3.zero - boidPos), Time.deltaTime * boidComponent.rotationSpeed);    
            } 
            else {

                Entities.WithAll <BoidComponent> ().ForEach ((ref Translation neighbourTranslation, ref BoidComponent neighbourBoid, ref LocalToWorld neighLocalToWorld) => {
                    float neighDistance = math.distance (boidPos, neighbourTranslation.Value);
                    if (neighDistance <= coheDis) {
                        neighbourCount++;
                        cohesionDir += neighbourTranslation.Value;
                        avgForward += neighLocalToWorld.Forward;

                        Debug.DrawLine (boidPos, neighbourTranslation.Value);

                        if (neighDistance <= avoidDis) {
                            avoidDir += boidPos - neighbourTranslation.Value;
                        }

                        avgSpeed += neighbourBoid.speed;
                    }
                });
                boidComponent.neighbourCount = neighbourCount;
                if (neighbourCount > 0) {

                    cohesionDir /= neighbourCount;
                    avgForward /= neighbourCount;
                    
                    boidComponent.speed = avgSpeed / neighbourCount; 
                    boidComponent.speed = math.clamp (boidComponent.speed, 0, 2.5f);
                    float3 stirDir = (cohesionDir + avoidDir + avgForward) - boidPos;
                    
                    boidRot.Value = Quaternion.Slerp (boidRot.Value, Quaternion.LookRotation (stirDir), Time.deltaTime * boidComponent.rotationSpeed);
                }

            }
        });
    }
}
