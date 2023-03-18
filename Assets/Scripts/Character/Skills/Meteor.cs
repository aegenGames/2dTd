using System.Collections.Generic;
using UnityEngine;

public class Meteor : BaseSkill
{
	[SerializeField]
	private int _radiusDamage;
	[SerializeField]
	private int _damage = 100;
	[SerializeField]
	private int _damagePerUnit = 10;
	[SerializeField]
	private Animator _explosionAnimator;

	private Vector2 _placeSize;

	private void Awake()
	{
		_placeSize.y = Camera.main.orthographicSize;
		_placeSize.x = _placeSize.y / Screen.height * Screen.width;
	}

	public override void UseSkill()
	{
		Vector2 impactPlace;
		impactPlace.x = Random.Range(-_placeSize.x, _placeSize.x);
		impactPlace.y = Random.Range(-_placeSize.y, _placeSize.y);
		this.transform.position = impactPlace;

		List<Character> hitCharacters = SearchTargetsInPlace(impactPlace);
		int curDmg = _damage + hitCharacters.Count * _damagePerUnit;
		this.transform.position = impactPlace;
		_explosionAnimator.SetTrigger("StartExplosion");


		foreach (Character character in hitCharacters)
		{
			character.TakeDamage(curDmg);
		}
	}

	private List<Character> SearchTargetsInPlace(Vector2 place)
	{
		List<Character> hitCharacters = new List<Character>();
		foreach(Character target in _targets)
		{
			if(target.DistanceTo(place) <= _radiusDamage)
			{
				hitCharacters.Add(target);
			}
		}

		return hitCharacters;
	}
}