using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SkillTrigger : MonoBehaviour, IPointerDownHandler
{
	[SerializeField]
	private Image _icone;
	[SerializeField]
	private float _cooldownTime;

	private bool _isCooldown = false;
	public UnityEvent OnUsed;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (_isCooldown)
			return;

		OnUsed?.Invoke();
		StartCoroutine(Cooldown());
	}

	public IEnumerator Cooldown()
	{
		_isCooldown = true;
		float fillAmount = _icone.fillAmount;
		float deltaAmount = fillAmount / _cooldownTime;
		_icone.fillAmount = 0;
		while(_icone.fillAmount < fillAmount)
		{
			_icone.fillAmount += deltaAmount * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		_isCooldown = false;
	}
}