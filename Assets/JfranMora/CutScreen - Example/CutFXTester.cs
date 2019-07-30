using UnityEngine;

namespace JfranMora
{
	public class CutFXTester : MonoBehaviour
	{
		Vector2 lastMousePos;
		Vector2 lastScreenPos;

		void Update()
		{
			Vector2 screenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			var direction = (screenPos - lastScreenPos).normalized;

			if (Input.GetMouseButtonDown(0))
			{
				FindObjectOfType<CutScreenFX>().DoFX(screenPos, direction);
			}

			lastScreenPos = screenPos;
		}
	}
}