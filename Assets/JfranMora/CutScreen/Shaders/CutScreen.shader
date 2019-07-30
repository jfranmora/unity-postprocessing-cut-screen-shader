Shader "Hidden/JfranMora/CutScreen"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Offset;
        float _Intensity;
        float _Blending;
        float _Angle;
        float2 _CutCenter;
        float2 _CutDirection;

        float2 rotateUV(float2 uv, float2 center, float angle)
        {
            // [-.5, .5]
            uv.xy -= .5;
            uv.xy -= center - .5;

            float s, c;
            sincos(radians(angle), s, c);
            float2x2 rotationMatrix = float2x2(c, -s, s, c);
            uv = mul(uv, rotationMatrix);

            // [0, 1]
            uv += .5;

            return uv;
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            // Mask UV
            float2 maskUV = rotateUV(i.texcoord, _CutCenter, _Angle);
            float mask = smoothstep(.5 - _Blending, .5 + _Blending, maskUV.y);
            float maskN = mask * 2 - 1;

            // Cut direction
            float2 cutDirection = _CutDirection * _Intensity * maskN * _Offset;
            
            // Color
            float2 uv = i.texcoord + cutDirection;
            float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
            return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}
