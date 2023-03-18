using System.Collections.Generic;

public interface ISkill
{
	public void SetTarget(Character target);
	public void SetTargets(List<Character> targets);
	public void UseSkill();
}