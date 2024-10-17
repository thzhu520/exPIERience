// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GeomFlow"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_FlowSpeed("Speed", Float) = 1
	}
		SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "TreeTransparentCutout"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 worldPosition : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _FlowSpeed;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
			{
				v2f test = (v2f)0;
				float3 normal = normalize(cross(input[1].worldPosition.xyz - input[0].worldPosition.xyz, input[2].worldPosition.xyz - input[0].worldPosition.xyz));
				
				float3 lightDir = float3(1, 1, 0);
				float ndotl = dot(normal, normalize(lightDir));

				for (int i = 0; i < 3; i++)
				{
					float3 relPos = input[i].vertex.xyz - input[i].worldPosition.xyz;
					test.normal = normal;
					test.vertex = input[i].vertex + float4(sin(_Time.y + 5 * relPos.x) * _FlowSpeed / 20, 0, 0, 0);
					test.uv = input[i].uv;
					OutputStream.Append(test);
				}
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;
				if (col.a < 0.8) {
					col.a = 0;
				}
				return col;
			}
			ENDCG
		}
	}
}