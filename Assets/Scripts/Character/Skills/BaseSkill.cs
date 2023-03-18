using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour, ISkill
{
	protected List<Character> _targets;

	public virtual void SetTarget(Character target)
	{
		List<Character> targets = new List<Character>() { target };
		SetTargets(targets);
	}

	public virtual void SetTargets(List<Character> targets)
	{
		_targets = targets;
	}

	public virtual void UseSkill()
	{
	}
}
