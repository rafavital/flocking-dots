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
            if (boidComponent.returning) return;

            float3 boidPos = boidTrans.Value;
            float coheDis = boidComponent.cohesionDistance;
            float avoidDis = boidComponent.avoidanceDistance;

            List <float3> cohesionNeighbours = new List<float3> ();
            float3 cohesionDir = float3.zero;
            float3 avoidDir = float3.zero;
            float3 avgForward = float3.zero;
            float avgSpeed = 0.01f;

            int neighbourCount = 0;

            Entities.WithAll <BoidComponent> ().ForEach ((ref Translation neighbourTranslation, ref BoidComponent neighbourBoid) => {
                float distance = math.distance (boidPos, neighbourTranslation.Value);
                if (distance <= coheDis) {
                    if (distance <= 0.1f) return;
                    neighbourCount++;
                    cohesionDir += neighbourTranslation.Value;
                    avgForward += neighbourTranslation.Value;

                    Debug.DrawLine (boidPos, neighbourTranslation.Value);

                    if (distance <= avoidDis) {
                        avoidDir += boidPos - neighbourTranslation.Value;
                    }

                    avgSpeed += neighbourBoid.speed;
                }
            });
            boidComponent.neighbourCount = neighbourCount;
            if (neighbourCount > 0) {

                cohesionDir /= neighbourCount;
                avgForward /= neighbourCount;
                avoidDir /= neighbourCount;
                
                boidComponent.speed = avgSpeed / neighbourCount; 
                boidComponent.speed = math.clamp (boidComponent.speed, 0, 1);
                float3 moveDir = math.normalize(cohesionDir + avoidDir + avgForward - boidPos) ;
                boidComponent.stirDir = cohesionDir;

                Debug.DrawRay (boidPos, cohesionDir, Color.blue);
                Debug.DrawRay (boidPos, avoidDir, Color.red);
                Debug.DrawRay (boidPos, moveDir, Color.green);

                boidComponent.velocity = moveDir;
                if (UnityEngine.Random.Range (0, 5) <= 1)
                    boidRot.Value = Quaternion.Slerp (boidRot.Value, Quaternion.LookRotation (moveDir), Time.deltaTime * boidComponent.rotationSpeed);

            }
        });
    }
}
