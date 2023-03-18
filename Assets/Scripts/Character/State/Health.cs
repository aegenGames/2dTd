using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField]
	private int _maxHealth;
	[SerializeField]
	[Min(0)]
	private int _healthPoints;

	[SerializeField]
	private Image _healthImage;

	public int HealthPoints
	{
		get => _healthPoints;
		set
		{
			_healthPoints = value <= MaxHealth ? value : MaxHealth;
			if(_healthImage != null)
				_healthImage.fillAmount = (float)_healthPoints / MaxHealth;
		}
	}

	public int MaxHealth { get => _maxHealth; }

	private void OnEnable()
	{
		HealthPoints = MaxHealth;
	}

	public void ResetHealth()
	{
		HealthPoints = MaxHealth;
	}
}