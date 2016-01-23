using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour, IPlayerControls 
{
	Player mSelectedPlayer;
	IGamePad mGamePad;
	IPlayerControlsView mView;

	public Team mTeam;

	Puck mPuck;

	public float mChargeSpeed;
	public float SELECTION_RESET_TIME;

	List<Player> sortedPlayersByDistance = new List<Player>();
	int mSelectIndex = 0;

	void Start()
	{
		mPuck = Puck.Instance;
		mGamePad = new WindowsGamePad(1, 0.1f);
		mView = GetComponent(typeof(IPlayerControlsView)) as IPlayerControlsView;
		if(mTeam.mPlayers.Count > 0)
		{
			SelectPlayer(mTeam.mPlayers[0]);
		}
		else
			Debug.LogError("Player: "+this.name+" has no team");

		sortedPlayersByDistance = new List<Player>(mTeam.mPlayers);
		sortedPlayersByDistance.Sort(delegate(Player x, Player y)
		                             {
			float distanceX = Vector2.Distance(x.GetPosition(), mPuck.transform.position);
			float distanceY = Vector2.Distance(y.GetPosition(), mPuck.transform.position);
			return distanceX.CompareTo(distanceY);
		});

		mPuck.PuckControlChanged += new PuckControlChangedHandler(OnPlayerRecievedPuck);
	
	}

	void OnPlayerRecievedPuck(Player p)
	{
		if(mTeam.mPlayers.Contains(p))		                     
			SelectPlayer(p);
	}


	void Update()
	{
		if(mSelectedPlayer != null)
		{
			MoveSelectedPlayer();
			//release shot
			if(mGamePad.IsButtonPressed(EGamePadButton.A))
			{
				if(mSelectedPlayer == mPuck.controllingPlayer)
					ChargeShot();
			}
			if(mGamePad.IsButtonPressed(EGamePadButton.RShoulder))
			{
				Player newPlayer = ChangeSelection();
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

		while(mGamePad.IsButtonDown(EGamePadButton.A))
		{
			pwr += mChargeSpeed*Time.deltaTime/Time.timeScale;
			if(pwr > 1) pwr = 1;
			if(mGamePad.GetLeftStick().normalized.magnitude > 0.5f)
			{
				direction = mGamePad.GetLeftStick().normalized;
			}
			mView.ShowChargeUpShot(pwr, mSelectedPlayer.transform.position, direction);
			yield return null;
		}

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale * fixedTimeScale;

		mView.ShowChargeUpShot(0, Vector2.zero, Vector2.zero);
		mPuck.Shoot(direction*pwr*mPuck.controllingPlayer.shotPower);
	}

	public void MoveSelectedPlayer()
	{
		Vector2 destination;
		Vector2 axis = mGamePad.GetLeftStick();
		
		if(axis != Vector2.zero)
		{
			destination = mSelectedPlayer.GetPosition() + (axis*mSelectedPlayer.speed);
			
			Debug.DrawLine(mSelectedPlayer.GetPosition(), destination);
		}
		else
			destination = mSelectedPlayer.GetPosition() - mSelectedPlayer.GetComponent<Rigidbody2D>().velocity/10f;
		
		mSelectedPlayer.destinationPosition = destination;
	}

	public Player GetSelectedPlayer()
	{
		return mSelectedPlayer;
	}

	public void SelectPlayer(Player Player)
	{
		mSelectedPlayer = Player;
		//mView.ChangeSelected(mSelectedPlayer);
	}
	

	
	IEnumerator ResetList(float time)
	{
		yield return new WaitForSeconds(time);
		sortedPlayersByDistance = new List<Player>(mTeam.mPlayers);
		sortedPlayersByDistance.Sort(delegate(Player x, Player y)
		{
			float distanceX = Vector2.Distance(x.GetPosition(), mPuck.transform.position);
			float distanceY = Vector2.Distance(y.GetPosition(), mPuck.transform.position);
			return distanceX.CompareTo(distanceY);
		});
		mSelectIndex = 0;
	}
	
	Player ChangeSelection()
	{
		StopCoroutine("ResetList");
		StartCoroutine("ResetList", SELECTION_RESET_TIME);

		Player result = sortedPlayersByDistance[mSelectIndex];

		mSelectIndex++;
		mSelectIndex %= sortedPlayersByDistance.Count;

		return result;
		
	}
}
