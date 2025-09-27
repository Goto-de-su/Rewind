Shader "Universal Render Pipeline/WobbleSprite"
{
	Properties{
		_BaseMap ("Sprite", 2D) = "white" {}
		_BaseColor ("Color", Color) = (1,1,1,1)
		_Intensity ("Intensity", Range(0,0.02)) = 0.005
		_Frequency ("Frequency", Range(0,50)) = 12
		_Speed ("Speed", Range(0,10)) = 1
		_AngleDeg ("Direction(deg)", Range(0,360)) = 0
	}
	SubShader{
		Tags{ "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off Cull Off
		Pass{
			Name "Unlit"
			Tags{ "LightMode"="UniversalForward" }
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			TEXTURE2D(_BaseMap); SAMPLER(sampler_BaseMap);
			float4 _BaseMap_ST; float4 _BaseColor;
			float _Intensity, _Frequency, _Speed, _AngleDeg;

			struct Attributes{ float4 positionOS:POSITION; float2 uv:TEXCOORD0; float4 color:COLOR; };
			struct Varyings{ float4 positionHCS:SV_POSITION; float2 uv:TEXCOORD0; float4 color:COLOR; };

			Varyings vert(Attributes v){
				Varyings o; o.positionHCS = TransformObjectToHClip(v.positionOS);
				o.uv = TRANSFORM_TEX(v.uv, _BaseMap); o.color = v.color; return o;
			}
			half4 frag(Varyings i):SV_Target{
				float t = _Time.y * _Speed;
				float rad = radians(_AngleDeg);
				float2 dir = float2(cos(rad), sin(rad));
				float s = sin((i.uv.x + i.uv.y) * _Frequency + t);
				float2 uv = i.uv + dir * s * _Intensity;
				half4 c = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv) * _BaseColor * i.color;
				return c;
			}
			ENDHLSL
		}
	}
}
