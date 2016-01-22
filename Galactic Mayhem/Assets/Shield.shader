Shader "Custom/Shield" {
	Properties 
	{
		_Color("Color", Color) = (1,1,1,1)
		_RimColor("RimColor", Color) = (1, 1, 1, 1)
		_RimPower("RimPower", Range(0.0001, 1000)) = 1
		_RimIntensity("RimIntensity", Float) = 1
		_Speed("Speed", Float) = 1
		_MainTex ("Main Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass 
		{
			Tags { "RenderType" = "Transparent" }
			Blend SrcAlpha One
			ZWrite Off
			Cull Off
			LOD 200

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			fixed4 _Color, _RimColor;
			float _Scale, _RimPower, _RimIntensity, _Speed;
			sampler2D _MainTex;

			float4 _Position0;
			float4 _Position1;
			float4 _Position2;
			float4 _Position3;
			float4 _Position4;
			float4 _Position5;
			float4 _Position6;
			float4 _Position7;
			float4 _Position8;
			float4 _Position9;
			float4 _Position10;
			float4 _Position11;
			float4 _Position12;
			float4 _Position13;
			float4 _Position14;
			float4 _Position15;

			float _Intensity0;
			float _Intensity1;
			float _Intensity2;
			float _Intensity3;
			float _Intensity4;
			float _Intensity5;
			float _Intensity6;
			float _Intensity7;
			float _Intensity8;
			float _Intensity9;
			float _Intensity10;
			float _Intensity11;
			float _Intensity12;
			float _Intensity13;
			float _Intensity14;
			float _Intensity15;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
				float3 pos : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
			};

			float calcIntensity(float3 shieldPos, float4 impact, float intensity) 
			{
				// float theta = impact.x;
				// float phi = impact.y;
				// float rho = 1;
				float factor = intensity;
// 
				// float3 impactPos;
				// impactPos.x = rho * cos(theta) * sin(phi);
				// impactPos.y = rho * sin(theta) * sin(phi);
				// impactPos.z = rho * cos(phi);

				float3 impactPos = impact.xyz;
				float impactRadius = impact.w / _Scale;

				float3 dist = impactPos - shieldPos;
				
				float distFactor = 0;
				float distFactor2 = 0;
				
				if (length(dist) < impactRadius)
				{
					distFactor = (length(dist)) / impactRadius;
					

					// distFactor += pow((impactRadius - length(dist)) / impactRadius, 2);

					distFactor *= distFactor * distFactor;
				}				
				
				return distFactor * factor * factor + distFactor2 * factor * factor;
			}

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = mul((float3x3)_Object2World, v.normal);
				o.texcoord = v.texcoord + half2(1, 0) * _Speed * _Time;
				o.color = v.color;
				o.pos = (v.normal).xyz;
				o.worldPos = mul(_Object2World, v.vertex).xyz;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float intensity = 0;

				intensity += calcIntensity(i.pos.xyz, _Position0, _Intensity0);
				intensity += calcIntensity(i.pos.xyz, _Position1, _Intensity1);
				intensity += calcIntensity(i.pos.xyz, _Position2, _Intensity2);
				intensity += calcIntensity(i.pos.xyz, _Position3, _Intensity3);
				intensity += calcIntensity(i.pos.xyz, _Position4, _Intensity4);
				intensity += calcIntensity(i.pos.xyz, _Position5, _Intensity5);
				intensity += calcIntensity(i.pos.xyz, _Position6, _Intensity6);
				intensity += calcIntensity(i.pos.xyz, _Position7, _Intensity7);
				intensity += calcIntensity(i.pos.xyz, _Position8, _Intensity8);
				intensity += calcIntensity(i.pos.xyz, _Position9, _Intensity9);
				intensity += calcIntensity(i.pos.xyz, _Position10, _Intensity10);
				intensity += calcIntensity(i.pos.xyz, _Position11, _Intensity11);
				intensity += calcIntensity(i.pos.xyz, _Position12, _Intensity12);
				intensity += calcIntensity(i.pos.xyz, _Position13, _Intensity13);
				intensity += calcIntensity(i.pos.xyz, _Position14, _Intensity14);
				intensity += calcIntensity(i.pos.xyz, _Position15, _Intensity15);

				// Rim
				float3 viewdir = normalize(_WorldSpaceCameraPos - i.worldPos);
				float ang = 1 - (abs(dot(viewdir, normalize(i.normal))));
				half4 rimCol = _RimColor * pow(ang, _RimPower) * _RimIntensity;

				half4 texColor = tex2D(_MainTex, i.texcoord);

				return (_Color * i.color * intensity * texColor + rimCol) * texColor;
			}
			ENDCG
		}
	}
}
