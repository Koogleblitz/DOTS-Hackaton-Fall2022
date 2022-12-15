using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class Bake_NpKart : MonoBehaviour
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







public class NpKartBaker: Baker<Bake_NpKart>
{
    public override void Bake(Bake_NpKart author)
    {

        AddComponent(new Cmpt_NpKart
        {
            attraction = author.attraction,
            repulsion = author.repulsion,
            socialDistance= author.socialDistance,
            peerPressure= author.peerPressure,
            boundary = author.boundary,
            towardsPlayer= author.towardsPlayer,
            radar= author.radar,
            speedLimit= author.speedLimit,
            speed= author.speed,
            position= author.position,
            velocity= author.velocity,
            step= author.step,
            targetPos= author.targetPos,
            randomness= author.randomness,
            followWeight= author.followWeight,

        }) ;
    }

}