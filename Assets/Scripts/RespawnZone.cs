using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
	[SerializeField]
	private Vector2 _respawnZoneCenter = new Vector2(0, 0);
	private Vector2 _respawnZoneSize;

	private void Awake()
	{
		_respawnZoneSize.y = Camera.main.orthographicSize;
		_respawnZoneSize.x = Camera.main.orthographicSize / Screen.height * Screen.width;
	}

	public void Respawn(List<Character> characters)
	{
		foreach (Character character in characters)
		{
			Vector2 respPoint = new Vector2(Random.Range(-_respawnZoneSize.x, _respawnZoneSize.x),
											Random.Range(-_respawnZoneSize.y, _respawnZoneSize.y));

			respPoint += _respawnZoneCenter;
			character.gameObject.SetActive(true);
			character.transform.position = respPoint;
		}
	}
}