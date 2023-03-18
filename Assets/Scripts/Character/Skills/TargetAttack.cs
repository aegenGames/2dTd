using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetAttack : BaseSkill, ISkill
{
	[SerializeField]
	private int _damage;
	[SerializeField]
	private int _radiusAttack;
	[SerializeField]
	private int _maxTargets;
	[SerializeField]
	private LineRenderer _visualEffect;
	[SerializeField]
	private float _effectDuration = 0.5f;

	[SerializeField]
	[SerializeInterface(typeof(IEffect))]
	private List<Object> effects;
	private List<IEffect> _effects => effects.OfType<IEffect>().ToList();

	public override void UseSkill()
	{
		if (_targets == null)
			return;

		base.UseSkill();

		Vector2 pos = this.transform.position;
		_targets.Sort((x, y) => x.DistanceTo(pos).CompareTo(y.DistanceTo(pos)));

		int num = _maxTargets > _targets.Count ? _targets.Count : _maxTargets;
		List<Character>  hitTargets = _targets.GetRange(0, num);
		for(int i = 0; i < num; ++i)
		{
			if(hitTargets[i].DistanceTo(this.transform.position) > _radiusAttack)
			{
				hitTargets.RemoveRange(i, hitTargets.Count - i);
				break;
			}

			hitTargets[i].TakeDamage(_damage);
			ApplyEffects(hitTargets[i]);
		}

		StartCoroutine(UseVisualEffect(hitTargets));
	}

	private IEnumerator UseVisualEffect(List<Character> targets)
	{
		if (_visualEffect == null)
			yield break;

		_visualEffect.positionCount += targets.Count();
		float deltaTime = 0;

		while (deltaTime < _effectDuration)
		{
			for (int i = 0; i < targets.Count; ++i)
			{
				_visualEffect.SetPosition(i + 1, targets[i].transform.position / _visualEffect.transform.lossyScale.x);
			}
			yield return new WaitForEndOfFrame();
			deltaTime += Time.deltaTime;
		}

		_visualEffect.positionCount = 1;
	}

	private void ApplyEffects(Character target)
	{
		foreach(IEffect effect in _effects)
		{
			effect.UseEffect(target);
		}
	}
}