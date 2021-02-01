using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	private int								_health;
	public int								index;

	public List<Waypoint>					wayPoints;
	public List<Waypoint>					neighbors;
	public List<Combatant>					touristsNearby;
	public List<Combatant>				    godsNearby;

	public enum EState						{ God, Tourist}

	private EState _currentState = EState.God;

    public EState CurrentState { get => _currentState;
        set => _currentState = value;
    }
	public int Health { get => _health;
        set => _health = value;
    }

	private void Awake()
	{
		var rends = GetComponentsInChildren<Renderer>();
		foreach (var rend in rends)
		{
			rend.enabled = false;
		}
	}


	public Vector3 Pos => transform.position;

    private void OnDrawGizmos()
	{
		if (neighbors.Count <= 0)
			return;
		Gizmos.color = new Color (0f, 0f, 0f);
		foreach (var neighbor in neighbors.Where(neighbor => neighbor.isActiveAndEnabled))
        {
            Gizmos.DrawLine (transform.position, neighbor.transform.position);
        }
	}
	internal void UpdateGodsNearby(bool addToList, Combatant god)
	{
		var alreadyInList = godsNearby.Contains(god);

        switch (addToList)
        {
            // Add tourist if the method is to add from the list, and the tourist is not already in the list
            case true when !alreadyInList:
                godsNearby.Add(god);
                break;
            // Remove tourist if the method is to remove from the list, and the tourist is already in the list
            case false when alreadyInList:
                godsNearby.Remove(god);
                break;
        }
    }


	internal void UpdateTouristsNearby(bool addToList, Combatant tourist)
	{
		var alreadyInList = touristsNearby.Contains(tourist);

        switch (addToList)
        {
            // Add tourist if the method is to add from the list, and the tourist is not already in the list
            case true when !alreadyInList:
                touristsNearby.Add(tourist);
                break;
            // Remove tourist if the method is to remove from the list, and the tourist is already in the list
            case false when alreadyInList:
                touristsNearby.Remove(tourist);
                break;
        }
    }
}
