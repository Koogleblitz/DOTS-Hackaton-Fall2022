using Unity.Entities;
using Unity.Mathematics;

public struct Cmpt_NpKart : IComponentData
{
    public float attraction;
    public float repulsion;
    public float socialDistance;
    public float peerPressure;
    public float boundary;
    public float towardsPlayer;
    public float radar;
    public float speedLimit;
    public float speed;
    public float3 position;
    public float3 velocity;
    public float step;
    public float3 targetPos;
    public float randomness;
    public float followWeight;


}
