
using UnityEngine;
using System.Collections;

public class SteeringBehavior 
{
	Rigidbody2D mRigidbody;
	float maxSpeed;

	public SteeringBehavior(Rigidbody2D rb, float ms)
	{
		mRigidbody = rb;
		maxSpeed = ms;
	}

	//**************************************************************************************************

	public Vector2 Seek(Vector2 target)
	{
		Vector2 pos2D = mRigidbody.transform.position;
		Vector2 desiredVelocity = (target - pos2D).normalized * maxSpeed;
		return (desiredVelocity - mRigidbody.velocity);
	}

	//**************************************************************************************************

	public Vector2 Flee(Vector2 target, float distance)
	{
		Vector2 pos2D = mRigidbody.transform.position;

		if(Vector2.Distance(target, pos2D) > distance)
			return Vector2.zero;

		Vector2 desiredVelocity = (pos2D - target).normalized * maxSpeed;
		return (desiredVelocity - mRigidbody.velocity);
	}

	//**************************************************************************************************
	
	public Vector2 Arrive(Vector2 targetPos, Deceleration deceleration)
	{
		Vector2 pos2D = mRigidbody.transform.position;
		Vector2 toTarget = targetPos - pos2D;
		
		float dist = toTarget.magnitude;
		
		if(dist > 0)
		{
			const float decelerationTweaker = 0.3f;
			float speed = dist / ((float)deceleration * decelerationTweaker);
			speed = Mathf.Min(maxSpeed, speed);
			
			Vector2 desiredVelocity = toTarget * speed / dist;
			
			return (desiredVelocity - mRigidbody.velocity);
		}
		
		return Vector2.zero;
	}

	public enum Deceleration{slow = 3, normal = 2, fast = 1};

	//**************************************************************************************************
	
	public Vector2 Pursuit(Rigidbody2D evader)
	{
		Vector2 evaderPos2D = evader.transform.position;
		Vector2 pos2D = mRigidbody.transform.position;
		
		Vector2 toEvader = evaderPos2D - pos2D;
		float relativeHeading = Vector2.Dot(mRigidbody.velocity.normalized, evader.velocity.normalized);
		
		if( Vector2.Dot(toEvader, mRigidbody.velocity.normalized) > 0 && relativeHeading < -0.95)
		{
			return Seek(evaderPos2D);
		}
		
		float lookAheadTime = toEvader.magnitude / (maxSpeed + evader.velocity.magnitude);
		
		return Seek (evaderPos2D + evader.velocity * lookAheadTime);
	}

	//**************************************************************************************************

	public Vector2 Evade(Rigidbody2D pursuer, float distance)
	{
		Vector2 pursuerPos2D = pursuer.transform.position;
		Vector2 pos2D = mRigidbody.transform.position;
		
		Vector2 toPursuer = pursuerPos2D - pos2D;		
		float lookAheadTime = toPursuer.magnitude / (maxSpeed + pursuer.velocity.magnitude);
		
		return Flee (pursuerPos2D + pursuer.velocity * lookAheadTime, distance);
	}

	//**************************************************************************************************

	public Vector2 Interpose(Rigidbody2D agentA, Rigidbody2D agentB)
	{
		Vector2 midPoint = (agentA.transform.position + agentB.transform.position)/2.0f;
		Vector2 pos2D = mRigidbody.transform.position;
		Vector2 aPos2D = agentA.transform.position;
		Vector2 bPos2D = agentB.transform.position;

		float timeToReachMidpoint = Vector2.Distance(pos2D, midPoint) / maxSpeed;

		Vector2 futureAPos = aPos2D + mRigidbody.velocity * timeToReachMidpoint;
		Vector2 futureBPos = bPos2D + mRigidbody.velocity * timeToReachMidpoint;

		midPoint = (futureAPos + futureBPos)/2.0f;
		return Arrive (midPoint, Deceleration.fast);
	}

}