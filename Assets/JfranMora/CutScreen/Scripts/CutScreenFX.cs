using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using JfranMora.Rendering.PostProcessing;

public class CutScreenFX : MonoBehaviour
{
	public AnimationCurve curve;
	public float fxDuration = .25f;

	[Header("References")]
	public PostProcessVolume volume;

	private CutScreen _cutScreen;

	#region Unity events

	private void Awake()
	{
		volume.profile.TryGetSettings(out _cutScreen);
	}

	#endregion

	#region Public API

	public void DoFX(Vector2 screenPos, Vector2 direction)
	{
		_cutScreen.cutCenter.value = screenPos;
		_cutScreen.cutDirection.value = direction;
		_cutScreen.angle.value = Vector2.SignedAngle(Vector2.right, direction);

		StartCoroutine(DoFXRoutine(fxDuration));	
	}

	#endregion

	private IEnumerator DoFXRoutine(float duration)
	{
		float currentTime = 0;

		while (currentTime < duration)
		{
			_cutScreen.intensity.value = curve.Evaluate(currentTime / duration);

			currentTime += Time.deltaTime;
			yield return null;
		}

		_cutScreen.intensity.value = 0;
	}
}
