using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Team team;

	public float speed;
	public float shotPower;
    int checkPower;
    int balance;

    public Vector2 puckCtrl;
	public SteeringBehavior Steering;
	
    protected Rigidbody2D m_Rigidbody;
    protected Vector2 DestinationPosition;

    // Use this for initialization
    public void Init () 
	{
        m_Rigidbody = GetComponent<Rigidbody2D>();
        Steering = new SteeringBehavior(m_Rigidbody, speed);
		checkPower = Random.Range (10, 20);
		balance = Random.Range(10, 20);
	}

    public void MoveTo(Vector2 dest)
    {
        DestinationPosition = dest;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	public bool RollForChecked()
	{
		int balanceRoll = (Puck.Instance.controllingPlayer.balance - 10)/2 + Random.Range(0,20);
		int checkRoll = (checkPower - 10)/2 + Random.Range(0,20);
		if(checkRoll + 2 > balanceRoll)
			return true;
		return false;
	}
}
