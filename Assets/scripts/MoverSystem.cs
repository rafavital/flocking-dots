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
        Entities.ForEach ((ref Translation translation, ref BoidComponent boid) => {
            if (math.distance (translation.Value, Vector3.zero) >= 10) {
                boid.returning = true;
                boid.velocity = math.normalize (float3.zero - translation.Value);
            } else {
                boid.returning = false;
            }
            translation.Value += boid.velocity * Time.deltaTime * boid.speed;
            

        });
    }
}
