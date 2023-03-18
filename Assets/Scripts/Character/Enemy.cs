using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
	[SerializeField]
	private MoveController _moveController;

	private bool _isAttacking;

	public UnityAction OnNearTarget;

	public override void SetTarget(Character character)
	{
		base.SetTarget(character);
		_moveController.Target = character == null ? null : character.transform;
		_autoattackSkill.SetTarget(character);
		_isAttacking = character == null? false : _collider.IsTouching(character.GetComponent<Collider2D>());
		if (_isAttacking)
			OnNearTarget?.Invoke();
	}

	protected override IEnumerator AutoAttack()
	{
		if (_autoattackSkill == null)
			yield break;

		while(true)
		{
			if(_isAttacking && _target != null)
			{
				_autoattackSkill.UseSkill();
				if (_target.tag == "Enemy" || _target.tag == "Boss")
					_target.SetTarget(this);
			}
			yield return new WaitForSeconds(_attackCooldown);
		}
	} 

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Character character = collision.gameObject.GetComponent<Character>();
		if (character == null)
			return;

		if (character.Equals(_target))
		{
			_isAttacking = true;
			OnNearTarget?.Invoke();
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		Character character = collision.gameObject.GetComponent<Character>();
		if (character == null)
			return;

		if (character.Equals(_target))
			_isAttacking = false;
	}
}