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
        Entities.ForEach ((ref Translation translation, ref BoidComponent boidComponent) => 
        {
            float3 boidPos = translation.Value;
            float cohesionDistance = boidComponent.cohesionDistance;

            Entities.WithAll <BoidComponent> ().ForEach ((ref Translation neighbourTranslation, ref BoidComponent neighbourComponent) => {
                if (math.distance (boidPos, neighbourTranslation.Value) <= cohesionDistance) {
                    //Add neighbours position to boid velocity vector
                }
            });
        });
    }
}
