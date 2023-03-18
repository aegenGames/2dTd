using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Player _player;
	[SerializeField]
	private Transform _enemyPool;
	[SerializeField]
	private RespawnZone _respawnZone;
	[SerializeField]
	private int _delayBefareRestart = 3;
	[SerializeField]
	private TextMeshProUGUI _endGameLable;

	private List<Character> _enemyList = new List<Character>();

	private void Awake()
	{
		_enemyPool.GetComponentsInChildren(true, _enemyList);
		_player.SetTargets(_enemyList);
		_player.OnDied += EndGame;
		foreach (Character character in _enemyList)
		{
			character.OnDied += CheckEnemies;
		}
	}

	private void Start()
	{
		LaunchWave();
	}

	private void CheckEnemies(Character character)
	{
		if (_enemyList.Count == 0)
		{
			LaunchWave();
		}
	}

	private void EndGame(Character player)
	{
		_endGameLable.gameObject.SetActive(true);
		foreach (Character character in _enemyList)
		{
			character.gameObject.SetActive(false);
		}
		StartCoroutine(Restart());
	}

	private IEnumerator Restart()
	{
		yield return new WaitForSeconds(_delayBefareRestart);
		_endGameLable.gameObject.SetActive(false);
		_player.gameObject.SetActive(true);
		LaunchWave();
	}

	private void LaunchWave()
	{
		List<Character> enemyList = new List<Character>();
		_enemyPool.GetComponentsInChildren(true, enemyList);
		_respawnZone.Respawn(enemyList);
		foreach(Character character in enemyList)
		{
			character.SetTarget(_player);
		}
	}
}