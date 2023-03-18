using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
	[SerializeField]
	protected Health _health;
	[SerializeField]
	protected Collider2D _collider;
	[SerializeField]
	[SerializeInterface(typeof(ISkill))]
	private Object autoattackSkill;
	protected ISkill _autoattackSkill => autoattackSkill as ISkill;
	[SerializeField]
	protected int _attackCooldown = 2;

	protected float _size;
	protected Character _target;

	public UnityAction<Character> OnDied;
	public UnityAction<Character> OnRespowned;

	private void OnValidate()
	{
		_collider = this.GetComponent<Collider2D>();
	}

	private void Awake()
	{
		_size = _collider.bounds.size.x;
	}

	public float DistanceTo(Vector2 point)
	{
		//"_size" for characters with differente size
		return Vector2.Distance(this.transform.position, point) - _size;
	}

	public virtual void SetTarget(Character character)
	{
		_target = character;
	}

	public Character GetTarget()
	{
		return _target;
	}

	private void OnEnable()
	{
		_health.ResetHealth();
		OnRespowned?.Invoke(this);
		StartCoroutine(AutoAttack());
	}

	protected virtual IEnumerator AutoAttack()
	{
		yield break;
	}

	public void TakeDamage(int damage)
	{
		_health.HealthPoints -= damage;
		if (_health.HealthPoints <= 0)
		{
			this.gameObject.SetActive(false);
			OnDied?.Invoke(this);
		}
	}

	public int TakeDamagePercent(int percent)
	{
		int damage = _health.MaxHealth * percent / 100;
		TakeDamage(damage);
		return damage;
	}

	public void TakeHeal(int heal)
	{
		_health.HealthPoints += heal;
	}
}