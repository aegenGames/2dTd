using UnityEngine;

public class MoveController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D _rig;
	[SerializeField]
	private Transform _target;
	[SerializeField]
	private float _speed;

	public Transform Target { get => _target; set => _target = value; }

	private void OnValidate()
	{
		_rig = this.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (Target == null)
		{
			_rig.velocity = Vector2.zero;
			return;
		}

		Vector2 direction = (Target.position - this.transform.position).normalized;
		_rig.velocity = direction * _speed;
	}
}