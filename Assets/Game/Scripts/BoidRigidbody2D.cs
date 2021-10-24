using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BoidRigidbody2D : MonoBehaviour
{
    //our boid's rigidbody
    public Rigidbody2D thisRigidbody2D;

    //predefined array to prevent unity GC, predefinied to keep GC low.
    private Collider2D[] surroundingColliders = new Collider2D[10];
    private int surroundingColliderNonAllocLength = 0;
    //filtered array of surrounding units, predefinied to keep GC low.
    private BoidRigidbody2D[] surroundingUnits = new BoidRigidbody2D[20];
    private int surroundingUnitsLength = 0;

    //adjustable control variables
    public float scanRadius = 1f;
    public float scanDistance = 0.5f;
    public float avoidRadius = 0.15f;
    public float avoidObjectsScanDistance = 1.0f;
    public float cohesionAmount = 0.5f;
    public float moveSpeed = 3f;
    public float maxSpeed = 5f;
    public float currentAvoidForce = 0;
    public float avoidForceProximityMultiplier = 100;
    public float avoidObjectDirectionFallOffSpeed = 20f;
    public float avoidObjectMultiplier = 20f;
    public float avergaeDirectionMultiplier = 5f;
    public float avoidDirectionMultiplier = 1f;

    //controls to enable or disable aspects of Boids algorithm
    public bool useAverageDirection;
    public bool useAvoidUnitsDirection;
    public bool useAvoidObjects;
    public bool useCohesion;

    //other variables for tracking an
    private Vector2 lastPos = Vector2.zero;
    private Vector2 avoidObjectDirection = Vector2.zero;

    //Unity inherited methods:

    /// <summary>
    /// Unity Start Method - Called at beginning of object's life, good time to initialize if required.
    /// </summary>
    private void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        //random start velocity to get some movement going.
        Vector2 randomVelocity = new Vector2(Mathf.PerlinNoise(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f)) * Random.Range(-1f, 1f),
                                             Mathf.PerlinNoise(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f)) * Random.Range(-1f, 1f)).normalized;
        thisRigidbody2D.velocity = randomVelocity.normalized;
    }
    /// <summary>
    /// Unity FixedUpdate Method - called in-step with unity physics engine.
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    //BoidsRigidbody2D methods:

    /// <summary>
    /// Scan for surrounding flockmates using a NonAllocOverlapCircle. NonAlloc used to reduce memory usage and GC collection.
    /// </summary>
    /// <param name="scanRadius"></param>
    void GetSurroundingUnits(float scanRadius)
    {
        surroundingColliderNonAllocLength = Physics2D.OverlapCircleNonAlloc(this.transform.position, scanRadius, surroundingColliders, 1 << LayerMask.NameToLayer("NPCS"));
        surroundingUnitsLength = 0;
        for (int i = 0; i < surroundingColliderNonAllocLength; i++)
        {
            //prevent including self
            if (surroundingColliders[i].gameObject != this.gameObject)
            {
                surroundingUnits[surroundingUnitsLength] = surroundingColliders[i].GetComponent<BoidRigidbody2D>();
                surroundingUnitsLength++;
            }
        }
    }/// <summary>
     /// Gets the average direction of local flockmates.
     /// </summary>
     /// <returns></returns>
    private Vector2 GetAverageDirection(float scanRadius)
    {
        if (!useAverageDirection)
            return Vector2.zero;

        Vector2 averageDirection = Vector2.zero;
        //use for loop instead of foreach, for loops are more optimized in Unity for memory management at high frame rate.
        for (int i = 0; i < surroundingUnitsLength; i++)
        {
            //get distance between this unit and surrounding unit
            float distance = Vector2.Distance(this.transform.position, surroundingUnits[i].transform.position);
            Vector2 velocity = surroundingUnits[i].currentDirection;//.thisRigidbody2D.velocity.normalized;
            velocity = velocity / distance;
            //add unit's direction to overall average. 
            averageDirection += velocity;
        }
        return averageDirection;
    }
    /// <summary>
    /// Gets direction opposite to avoid crowding and overlap of flockmates.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetAvoidDirection(float avoidRadius)
    {
        if (!useAvoidUnitsDirection)
            return Vector2.zero;

        Vector2 oppositeDirection = Vector2.zero;
        //use for loop instead of foreach, for loops are more optimized in Unity for memory management at high frame rate.
        for (int i = 0; i < surroundingUnitsLength; i++)
        {
            float distance = Vector2.Distance(this.transform.position, surroundingUnits[i].transform.position);
            if (distance <= avoidRadius)
            {
                Vector2 diff = this.transform.position - surroundingUnits[i].transform.position;
                diff.Normalize();
                diff = diff / distance;
                oppositeDirection += diff;
            }
        }
        return oppositeDirection;
    }
    public Vector2 currentDirection
    {
        get { return thisRigidbody2D.velocity.normalized; }
    }
    /// <summary>
    /// Gets direction opposite to avoid bumping into map objects (i.e. walls)
    /// </summary>
    /// <returns></returns>
    Vector2 ScanForObjectsToAvoid()
    {
        Vector2 avgDirection = Vector2.zero;
        Vector3 longestOpenPathDirection = Vector3.zero;
        Vector3 longestClosedPathDirection = Vector3.zero;
        bool hitSomething = false;
        currentAvoidForce = 1;
        float lastHitDistance = 0;

        //get angle of current heading to do some trig
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        
        //we're doing 3 raycasts
        for (int t = 0; t < 3; t++)
        {
            RaycastHit2D raycastHit2DResult;                                                                                                                                    // Gizmos.DrawLine(this.transform.position + new Vector3(0, -avoidRadius / 2f, 0), this.transform.position + (new Vector3(0, -avoidRadius / 2f, 0) + new Vector3(facingDirection.x, facingDirection.y, 0) * avoidRadius));
            Vector2 currentRayDir = Vector2.zero;
            if (t == 0)
            {
                //raycast forward
                currentRayDir = currentDirection * (avoidObjectsScanDistance + 0.5f);
                raycastHit2DResult = Physics2D.Raycast(this.transform.position, currentRayDir, avoidObjectsScanDistance + 0.5f, 1 << LayerMask.NameToLayer("Map"));
                Debug.DrawRay(this.transform.position, currentDirection * (avoidObjectsScanDistance + 0.5f), Color.blue, 0.1f);
            }
            else if (t == 1)
            {
                //upward relative angle to direction of movement to be used for raycast.
                Vector2 dir = DegreeToVector2(75 + angle);
                dir = (currentDirection + dir).normalized * avoidObjectsScanDistance;
                currentRayDir = dir;
                raycastHit2DResult = Physics2D.Raycast(this.transform.position, dir, avoidObjectsScanDistance, 1 << LayerMask.NameToLayer("Map"));
                Debug.DrawRay(this.transform.position, dir, Color.red, 0.1f);
            }
            else
            {
                //downward relative angle to direction of movement to be used for raycast.
                Vector2 dir = DegreeToVector2(-75 + angle);
                dir = (currentDirection + dir).normalized * avoidObjectsScanDistance;
                currentRayDir = dir;
                raycastHit2DResult = Physics2D.Raycast(this.transform.position, dir, avoidObjectsScanDistance, 1 << LayerMask.NameToLayer("Map"));
                Debug.DrawRay(this.transform.position, dir, Color.green, 0.1f);
            }
            //check if we hit anything and if mutliple raycasts hit something, we want to find the one furthest away.
            if (raycastHit2DResult.collider != null)
            {
                hitSomething = true;
                float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), raycastHit2DResult.point);
                //if this point was further than other hit points, we might potentially head in this direction
                if (dist > lastHitDistance)
                {
                    lastHitDistance = dist;
                    longestClosedPathDirection = currentRayDir;
                }
            }
            //if the raycast didn't hit anything then that is currently longest open path
            else
            {
                longestOpenPathDirection = currentRayDir;
            }
        }
        //if no raycasts hit anything, we should return nothing and keep heading in current direction.
        if (!hitSomething)
        {
            return Vector2.zero;
        }
        //if all three directional raycasts hit something, we'll head towards direction with furthest distance between self and hit point.
        else if (longestOpenPathDirection == Vector3.zero)
        {
            return longestClosedPathDirection;
        }
        //if one of the raycasts were open but others hit something, we'll head in direction of ray that didn't hit anything.
        else
        {
            return longestOpenPathDirection;
        }
    }
    /// <summary>
    /// Gets direction towards average position of local flockmates. 
    /// Can be used to keep boids closer or further apart.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetCohesionDirection(float cohesion)
    {
        if (!useCohesion)
            return Vector2.zero;

        Vector2 directionToCentre = Vector2.zero;
        Vector2 centrePosition = Vector2.zero;
        for (int i = 0; i < surroundingUnitsLength; i++)
        {
            float distance = Vector2.Distance(transform.position, surroundingUnits[i].transform.position);
            if (distance <= scanRadius)
            {
                centrePosition += new Vector2(surroundingUnits[i].transform.position.x, surroundingUnits[i].transform.position.y);
            }
        }
        if (surroundingUnitsLength > 0)
        {
            centrePosition = centrePosition / surroundingUnitsLength;
            directionToCentre = centrePosition - new Vector2(transform.position.x, transform.position.y);
            Debug.DrawRay(this.transform.position, directionToCentre.normalized * 2,Color.blue, 0.2f);
        }
        return directionToCentre * cohesion;
    }
    /// <summary>
    /// Move this boid by applying 2d physical force.
    /// </summary>
    private void Move()
    {
        //calculate direction we want to move to using boids 
        Vector2 moveDirection = BoidsMoveDirection();
        moveDirection *= moveSpeed;
        thisRigidbody2D.AddForce(moveDirection);
        //prevent going over max speed
        if (thisRigidbody2D.velocity.magnitude > maxSpeed)
            thisRigidbody2D.velocity = Vector2.ClampMagnitude(thisRigidbody2D.velocity, maxSpeed);

        // Rotate in direction of movement
        float rotation = Vector2.Angle(Vector2.up, thisRigidbody2D.velocity);
        float sign = Mathf.Sign(-thisRigidbody2D.velocity.x);
        rotation *= sign;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    /// <summary>
    /// Calculate the direction to move taking into account other surrounding flockmates and objects
    /// </summary>
    /// <returns></returns>
    private Vector2 BoidsMoveDirection()
    {
        Vector2 moveDirection = Vector2.zero;

        //check if any surrounding units.
        GetSurroundingUnits(scanRadius);
        //get direction this unit should move based on surrounding units.
        moveDirection += GetAverageDirection(scanDistance) * avergaeDirectionMultiplier;
        moveDirection += GetAvoidDirection(avoidRadius) * avoidDirectionMultiplier;

        //scan for objects to avoid and then smoothly adjust to prevent sudden hit in direction change.
        var avoidObjectDirectionT = ScanForObjectsToAvoid().normalized * avoidObjectMultiplier;
        if (avoidObjectDirectionT.magnitude > 0)
            avoidObjectDirection = avoidObjectDirectionT;
        avoidObjectDirection = Vector2.Lerp(avoidObjectDirection, Vector2.zero, Time.deltaTime * avoidObjectDirectionFallOffSpeed);
        if (avoidObjectDirection.magnitude < 0.01f)
        {
            avoidObjectDirection = Vector2.zero;
        }
        moveDirection += avoidObjectDirection;
        moveDirection += GetCohesionDirection(cohesionAmount);

        return moveDirection.normalized;
    }
    /// <summary>
    /// Convert radian to Vector2
    /// </summary>
    /// <param name="radian"></param>
    /// <returns></returns>
    public Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
    /// <summary>
    /// Convert degree to Vector2
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}