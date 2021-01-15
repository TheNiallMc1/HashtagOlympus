using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	private int								_health;
	public List<Waypoint>					neighbors;
	public GameObject						monument;
	public List<AI_Brain>					touristsNearby;
	public	List<GodBehaviour>				godsNearby;
	[SerializeField] private Vector3		finalLocation;
	public Vector3							distanceToFinal;
	public enum eState						{ God, Tourist}

	private eState _currentState = eState.God;

    public eState currentState { get { return _currentState; } set { _currentState = value; } }
	public int health { get { return _health; } set { _health = value; } }

	private void Awake()
	{
		Renderer[] rends = GetComponentsInChildren<Renderer>();
		foreach (Renderer rend in rends)
		{
			rend.enabled = false;
		}
	}


	public Vector3 pos
	{
		get
		{
			return transform.position;
		}
	}

	void OnDrawGizmos()
	{
		if (neighbors == null)
			return;
		Gizmos.color = new Color (0f, 0f, 0f);
		foreach(var neighbor in neighbors)
		{
			if (neighbor != null)
				Gizmos.DrawLine (transform.position, neighbor.transform.position);
		}
	}
	internal void UpdateGodsNearby(bool addToList, GodBehaviour god)
	{
		bool alreadyInList = godsNearby.Contains(god);

		// Add tourist if the method is to add from the list, and the tourist is not already in the list
		if (addToList && !alreadyInList)
		{
			godsNearby.Add(god);


		}

		// Remove tourist if the method is to remove from the list, and the tourist is already in the list
		if (!addToList && alreadyInList)
		{

			godsNearby.Remove(god);
		}
	}


	internal void UpdateTouristsNearby(bool addToList, AI_Brain tourist)
	{
		bool alreadyInList = touristsNearby.Contains(tourist);

		// Add tourist if the method is to add from the list, and the tourist is not already in the list
		if (addToList && !alreadyInList)
		{
			touristsNearby.Add(tourist);
		}



		// Remove tourist if the method is to remove from the list, and the tourist is already in the list
		if (!addToList && alreadyInList)
		{
			touristsNearby.Remove(tourist);
		}
	}
}
