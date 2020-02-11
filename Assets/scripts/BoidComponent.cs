using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public struct BoidComponent : IComponentData
{
    public float speed;
    public float maxSpeed;
    public float rotationSpeed;
    public float cohesionDistance;
    public float avoidanceDistance;
    public bool returning;
    public float3 cohesionDir;
    public float3 avoidDir;
    public float3 allignDir;
}
