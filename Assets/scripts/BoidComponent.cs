using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public struct BoidComponent : IComponentData
{
    public float speed;
    public float rotationSpeed;
    public float cohesionDistance;
    public float avoidanceDistance;
    public float neighbourCount;
    public bool returning;
    public float3 velocity;
    public float3 stirDir;
}
