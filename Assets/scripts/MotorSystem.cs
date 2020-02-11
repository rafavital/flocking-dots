using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MotorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach ((ref Translation translation, ref Rotation rotation, ref BoidComponent boid, ref LocalToWorld localToWorld) => {
            
            if (math.distance (translation.Value, float3.zero) >= 10) {
                boid.returning = true;
            } else {
                boid.returning = false;
            }
            translation.Value += localToWorld.Forward * Time.deltaTime * boid.speed;
            
            if (boid.returning) {
                    rotation.Value = Quaternion.Slerp (rotation.Value, Quaternion.LookRotation (float3.zero - translation.Value), Time.deltaTime * boid.rotationSpeed);    
            } else {
                float3 stirDir = (boid.cohesionDir + boid.avoidDir + boid.allignDir) - translation.Value;   
                rotation.Value = Quaternion.Slerp (rotation.Value, Quaternion.LookRotation (stirDir), Time.deltaTime * boid.rotationSpeed);
            }

        });
    }
}
