using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial class Sys_NpKart_Rand : SystemBase
{
    
    uint popCap= 100;
    uint cnt= 1;
    public uint cntLimit= 1000;


    protected override void OnUpdate(){

        float deltaTime= SystemAPI.Time.DeltaTime;
        Unity.Mathematics.Random rand= new Unity.Mathematics.Random(cnt/200 + 1);
        EntityQuery npKarts= EntityManager.CreateEntityQuery(typeof(Cmpt_NpKart));
        int population= npKarts.CalculateEntityCount();


        foreach ((TransformAspect transpect, RefRW<Cmpt_NpKart> npKart) in SystemAPI.Query<TransformAspect, RefRW<Cmpt_NpKart>>()){
            float3 selfPos= transpect.WorldPosition;
            float3 vel= npKart.ValueRW.velocity;
            float speed= npKart.ValueRW.speed;
            float randomness= npKart.ValueRW.randomness+0.0000000001f;
            float3 randRange= new float3(1,0,1)*200;
            float3 randPos= rand.NextFloat3(selfPos-randRange, selfPos+randRange);
            float3 distVector= (randPos- selfPos);
            float3 dir = math.normalize(distVector);
            float3 deltaPos= dir * speed * deltaTime * randomness;

            transpect.WorldPosition += deltaPos;
            transpect.LookAt(selfPos+distVector );
        }
        
        cnt= (cnt<=cntLimit)? cnt+1 : 0;

        // UnityEngine.Debug.Log(cnt);

    }
}