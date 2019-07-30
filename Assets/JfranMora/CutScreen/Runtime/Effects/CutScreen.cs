using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace JfranMora.Rendering.PostProcessing
{
	[Serializable]
	[PostProcess(typeof(CutScreenRenderer), PostProcessEvent.AfterStack, "JfranMora/CutScreen")]
	public sealed class CutScreen : PostProcessEffectSettings
	{
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter { value = 0f };

		[Range(0f, 1f)]
		public FloatParameter blending = new FloatParameter { value = 0f };

		[Range(0f, 1f)]
		public FloatParameter offset = new FloatParameter { value = 0f };

		[Range(0f, 360f)]
		public FloatParameter angle = new FloatParameter { value = 0f };

		public Vector2Parameter cutCenter = new Vector2Parameter { value = new Vector2(.5f, .5f) };

		public Vector2Parameter cutDirection = new Vector2Parameter { value = new Vector2(.5f, 0) };
	}

	public sealed class CutScreenRenderer : PostProcessEffectRenderer<CutScreen>
	{
		public override void Render(PostProcessRenderContext context)
		{
			var sheet = context.propertySheets.Get(Shader.Find("Hidden/JfranMora/CutScreen"));

			// Configurar
			sheet.properties.SetFloat("_Intensity", settings.intensity);
			sheet.properties.SetFloat("_Blending", settings.blending);
			sheet.properties.SetFloat("_Offset", settings.offset);
			sheet.properties.SetFloat("_Angle", settings.angle);
			sheet.properties.SetVector("_CutCenter", settings.cutCenter);
			sheet.properties.SetVector("_CutDirection", settings.cutDirection);

			// Blit
			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
		}
	}
}