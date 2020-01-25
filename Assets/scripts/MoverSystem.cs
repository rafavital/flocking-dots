using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach ((ref Translation translation, ref BoidComponent boid, ref LocalToWorld localToWorld) => {
            if (math.distance (translation.Value, float3.zero) >= 10) {
                boid.returning = true;
            } else {
                boid.returning = false;
            }
            translation.Value += localToWorld.Forward * Time.deltaTime * boid.speed;
            

        });
    }
}
