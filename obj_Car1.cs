using UnityEngine;

[RequireComponent(typeof(Obj_car))]
public class Obj_car1 : MonoBehaviour
{
    private Obj_car car;
    public UnityEngine.Vector3 objVel;
    public float objSpeedLim;
    public Vector3 initSpeed;
    public float radar;
    public float cohesionWeight;
    public float separationWeight;
    public float alignmentWeight;
    public float originGravity;
    public float playerGravity;
    public float boundary;
    public float playerField;
    public UnityEngine.Vector3 origin;
    public float speedMultiplier;
    private float dispersion;
    private uint cnt;
    public int cntLimit;
    // public GameObject player;
    // public UnityEngine.Vector3 playerPos;
    
    
    void Start()
    {
        objVel= initSpeed;
        car= GetComponent<Obj_car>();    
        cnt=1;


    }
    void Update(){
        var deltaTime= Time.deltaTime;


        var centroid= UnityEngine.Vector3.zero;
        var avgVel= UnityEngine.Vector3.zero;
        var selfPos= this.transform.position;
        var direction= selfPos.normalized;
        var displacement= (origin - selfPos).magnitude;
        var cars= FindObjectsOfType<Obj_car>();
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        var playerVector= (playerPos - selfPos);
        var playerDist= playerVector.magnitude;


        
        //playerPos=  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // playerPos= player.transform.position;

        int sampling= 0;

        if(cnt<(cntLimit-100)){
            dispersion= 0;
        }else{
            dispersion= 100;
        }

        foreach(var car in cars){
            var pos= this.transform.position;
            var carPos= car.transform.position;
            var directionVector= carPos-pos;
            var dist= directionVector.magnitude;
            if(dist<(radar+dispersion)){
                centroid+= directionVector;
                centroid+= playerVector*playerGravity/10;
                avgVel+= car.objVel;
                sampling+= 1;
            }
        } 
        centroid= centroid/sampling;
        avgVel= avgVel/sampling;


        objVel+= (UnityEngine.Vector3.Lerp(origin, centroid, (centroid.magnitude/radar)))*cohesionWeight;
        //objVel+= (UnityEngine.Vector3.Lerp(origin, playerPos, (playerPos.magnitude/radar)))*playerGravity;
        objVel-= (UnityEngine.Vector3.Lerp(origin, centroid, (radar/centroid.magnitude)))*separationWeight;
        objVel+= (UnityEngine.Vector3.Lerp(this.objVel, avgVel, deltaTime))*alignmentWeight;
        

        if(displacement > boundary){
            car.objVel-= (direction* displacement) * originGravity;
        }
        // if(playerDist > playerField){
        //     car.objVel+= (direction* playerField) * playerGravity;
        // }
        if(objVel.magnitude>objSpeedLim){
            objVel= objVel.normalized*objSpeedLim;
        }
        this.transform.position+= objVel*deltaTime*speedMultiplier;
        this.transform.rotation= UnityEngine.Quaternion.LookRotation(objVel);



        cnt= (cnt<cntLimit)? cnt+1 : 1;
        

        UnityEngine.Debug.Log(playerPos);
    }
}