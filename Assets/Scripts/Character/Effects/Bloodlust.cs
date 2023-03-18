using System.Collections;
using UnityEngine;

public class Bloodlust : MonoBehaviour, IEffect
{
	[SerializeField]
	[Range(1, 100)]
	private int _damagePercent = 5;
	[SerializeField]
	private Character _targetForHeal;
	[SerializeField]
	[Min(1)]
	private int _duration = 5;
	[SerializeField]
	[Min(0)]
	private int _cooldown = 5;

	private bool _isCooldown = false;

	private void OnEnable()
	{
		StartCoroutine(Cooldown());
	}

	public void UseEffect(Character target)
	{
		if (_isCooldown || target == null)
			return;

		int heal = target.TakeDamagePercent(_damagePercent);
		_targetForHeal.TakeHeal(heal);
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown()
	{
		while (true)
		{
			yield return new WaitForSeconds(_duration);
			_isCooldown = true;
			yield return new WaitForSeconds(_cooldown);
			_isCooldown = false;
		}
	}
}