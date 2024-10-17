

Shader "Custom/PilingDistortion"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_SecondTex("Above Water (RGB)", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
		_BumpPow("Bumpmap Power", Float) = 1
		_Speed("Speed", Float) = 1
		_YOffset("Y Scaling", Float) = 1

		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0	

		sampler2D _MainTex, _BumpMap, _SecondTex;
		float _Speed, _YOffset, _BumpPow;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_SecondTex;
			float2 uv_BumpMap;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			float time = _Time.y * _Speed;
			float2 distort, distortBump;

			distort.x = sin(time + (IN.uv_MainTex.y / _YOffset)) / 20 + IN.uv_MainTex.x;
			distort.y = IN.uv_MainTex.y;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			distortBump.x = sin(time + (IN.uv_BumpMap.y / _YOffset)) / 20 + IN.uv_BumpMap.x;
			distortBump.y = IN.uv_BumpMap.y;

			if (IN.worldPos.y > 0) {
				c = tex2D(_SecondTex, IN.uv_SecondTex);
			}
			else {
				o.Normal = UnpackNormal(tex2D(_BumpMap, distortBump) * _BumpPow);
			}

			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			
		}

		ENDCG
		}
		FallBack "Diffuse"
}

