using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Character
{

	[SerializeField]
	[SerializeInterface(typeof(ISkill))]
	private List<Object> skillList;
	private List<ISkill> _skillList => skillList.OfType<ISkill>().ToList();

	private List<Character> _targets;

	private void OnValidate()
	{
		if(!_skillList.Contains(_autoattackSkill) && _autoattackSkill != null)
		{
			Debug.LogError("Skill List in Player must contain Autoattack Skill");
		}
	}

	protected override IEnumerator AutoAttack()
	{
		if (_autoattackSkill == null)
			yield break;

		while (true)
		{
			_autoattackSkill.UseSkill();
			yield return new WaitForSeconds(_attackCooldown);
		}
	}

	public override void SetTarget(Character character)
	{
		base.SetTarget(character);
		List<Character> targets = new List<Character> { character };
		SetTargets(targets);
	}

	public void SetTargets(List<Character> targets)
	{
		if (targets.Count < 0)
			return;

		_targets = targets;
		foreach (Character target in _targets)
		{
			target.OnDied += RemoveTarget;
			target.OnRespowned += AddTarget;
		}

		foreach(ISkill skill in _skillList)
		{
			skill.SetTargets(_targets);
		}
	}

	private void RemoveTarget(Character character)
	{
		_targets.Remove(character);
	}

	private void AddTarget(Character character)
	{
		if(!_targets.Contains(character))
			_targets.Add(character);
	}
}