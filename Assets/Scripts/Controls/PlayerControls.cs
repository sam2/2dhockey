using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour, IPlayerControls 
{
	Skater m_SelectedPlayer;
	IGamePad m_Gamepads;
	IPlayerControlsView m_View;

	private Team m_Team;	

	public float CHARGE_SPEED;
	public float SELECTION_RESET_TIME;

	List<Skater> sortedPlayersByDistance = new List<Skater>();
	int mSelectIndex = 0;

    void Awake()
    {
        m_Gamepads = new WindowsGamePad(1, 0.1f);
        m_View = GetComponent(typeof(IPlayerControlsView)) as IPlayerControlsView;
        m_Team = GetComponent<Team>();
    }

	void Start()
	{	

		if(m_Team.mPlayers.Count > 0)
		{
			SelectPlayer(m_Team.mPlayers[0]);
		}
		else
			Debug.LogError("Player: "+this.name+" has no team");

		sortedPlayersByDistance = new List<Skater>(m_Team.mPlayers);
		sortedPlayersByDistance.Sort(delegate(Skater a, Skater b)
		                             {
			float distanceA = Vector2.Distance(a.GetPosition(), Puck.Instance.transform.position);
			float distanceB = Vector2.Distance(b.GetPosition(), Puck.Instance.transform.position);
			return distanceA.CompareTo(distanceB);
		});

		Puck.Instance.PuckControlChanged += new PuckControlChangedHandler(OnPlayerRecievedPuck);
	
	}

	void OnPlayerRecievedPuck(Skater p)
	{
		if(m_Team.mPlayers.Contains(p))		                     
			SelectPlayer(p);
	}


	void Update()
	{
		if(m_SelectedPlayer != null)
		{
			MoveSelectedPlayer();
			//release shot
			if(m_Gamepads.IsButtonPressed(EGamePadButton.A))
			{
				if(m_SelectedPlayer == Puck.Instance.controllingPlayer)
					ChargeShot();
			}
			if(m_Gamepads.IsButtonPressed(EGamePadButton.RShoulder))
			{
				Skater newPlayer = ChangeSelection();
				SelectPlayer(newPlayer);
			}
		}
	}

	void ChargeShot()
	{
		StartCoroutine(ChargeShotCoroutine());
	}

	IEnumerator ChargeShotCoroutine()
	{
		Time.timeScale = 0.2f;
		float fixedTimeScale = Time.fixedDeltaTime;
		Time.fixedDeltaTime = Time.timeScale * fixedTimeScale;

		Vector2 direction = Vector2.right;
		float pwr = 0.0f;

		while(m_Gamepads.IsButtonDown(EGamePadButton.A))
		{
			pwr += CHARGE_SPEED*Time.deltaTime/Time.timeScale;
			if(pwr > 1) pwr = 1;
			if(m_Gamepads.GetLeftStick().normalized.magnitude > 0.5f)
			{
				direction = m_Gamepads.GetLeftStick().normalized;
			}
			m_View.ShowChargeUpShot(pwr, m_SelectedPlayer.transform.position, direction);
			yield return null;
		}

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale * fixedTimeScale;

		m_View.ShowChargeUpShot(0, Vector2.zero, Vector2.zero);
		Puck.Instance.Shoot(direction*pwr*Puck.Instance.controllingPlayer.shotPower);
	}

	public void MoveSelectedPlayer()
	{
		Vector2 destination;
		Vector2 axis = m_Gamepads.GetLeftStick();
        Debug.Log(axis);
		
		if(axis != Vector2.zero)
		{
			destination = m_SelectedPlayer.GetPosition() + (axis*m_SelectedPlayer.speed);
			
			Debug.DrawLine(m_SelectedPlayer.GetPosition(), destination);
		}
		else
			destination = m_SelectedPlayer.GetPosition() - m_SelectedPlayer.GetComponent<Rigidbody2D>().velocity/10f;
		
		m_SelectedPlayer.MoveTo(destination);
	}

	public Skater GetSelectedPlayer()
	{
		return m_SelectedPlayer;
	}

	public void SelectPlayer(Skater player)
	{
		m_SelectedPlayer = player;
        player.ChangeState(player.controlledState);
	}
	

	
	IEnumerator ResetList(float time)
	{
		yield return new WaitForSeconds(time);
		sortedPlayersByDistance = new List<Skater>(m_Team.mPlayers);
		sortedPlayersByDistance.Sort(delegate(Skater x, Skater y)
		{
			float distanceX = Vector2.Distance(x.GetPosition(), Puck.Instance.transform.position);
			float distanceY = Vector2.Distance(y.GetPosition(), Puck.Instance.transform.position);
			return distanceX.CompareTo(distanceY);
		});
		mSelectIndex = 0;
	}
	
	Skater ChangeSelection()
	{
		StopCoroutine("ResetList");
		StartCoroutine("ResetList", SELECTION_RESET_TIME);

		Skater result = sortedPlayersByDistance[mSelectIndex];

		mSelectIndex++;
		mSelectIndex %= sortedPlayersByDistance.Count;

		return result;
		
	}
}
