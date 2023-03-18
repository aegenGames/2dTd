using System.Collections;
using UnityEngine;

public class Control : BaseSkill
{
	[SerializeField]
	private float _controlTime = 5;

	private Character _curentControl;

	public override void UseSkill()
	{
		if (_targets == null)
			return;

		base.UseSkill();
		StartCoroutine(UseControl());
	}

	private IEnumerator UseControl()
	{
		Character defaultTarget = _targets[0].GetTarget();
		_curentControl = null;

		if (_targets.Count == 1)
		{
			if (_targets[0].CompareTag("Boss"))
			{
				yield break;
			}
			else
			{
				_curentControl = _targets[0];
				_curentControl.SetTarget(null);
			}
		}
		else
		{
			while (_curentControl == null)
			{
				int i = Random.Range(0, _targets.Count);
				if (!_targets[i].CompareTag("Boss"))
				{
					_curentControl = _targets[i];
					_curentControl.SetTarget(GetNearEnemy(_curentControl.transform.position));
				}
			}
		}

		(_curentControl as Enemy).OnNearTarget += ReachedTarget;
		yield return new WaitForSeconds(_controlTime);
		_curentControl.GetTarget()?.SetTarget(defaultTarget);
		_curentControl.SetTarget(defaultTarget);
		(_curentControl as Enemy).OnNearTarget -= ReachedTarget;
	}

	private void ReachedTarget()
	{
		_curentControl.GetTarget().SetTarget(_curentControl);
	}

	private Character GetNearEnemy(Vector2 pos)
	{
		Character result = null;
		float dist = -1;

		for(int i = 0; i < _targets.Count; ++i)
		{
			if (_targets[i].Equals(_curentControl))
				continue;
			float tmpDist = Vector2.Distance(_targets[i].transform.position, pos);
			if (tmpDist < dist || dist < 0)
			{
				dist = tmpDist;
				result = _targets[i];
			}
		}

		return result;
	}
}