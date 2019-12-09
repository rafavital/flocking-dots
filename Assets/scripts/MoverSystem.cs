using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach ((ref Translation translation, ref BoidComponent boid) => {
            translation.Value += boid.velocity * Time.deltaTime;
        });
    }
}
