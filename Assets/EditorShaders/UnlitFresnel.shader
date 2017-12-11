Shader "Unlit Fresnel" {
	Properties{
		_MainColor("Colour", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_RimColor("Fresnel Color", Color) = (0.3,0.3,0.3,1.0)
		_RimPower("Fresnel Power", Range(0.5,8.0)) = 3.0
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
	#pragma surface surf Lambert noambient

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		sampler2D _MainTex;
		float4 _RimColor;
		float _RimPower;
		float4 _MainColor;

		void surf (Input IN, inout SurfaceOutput o) {
			o.Emission = tex2D (_MainTex, IN.uv_MainTex).rgb * _MainColor;
			half rim = 1.0 - saturate (dot (normalize (IN.viewDir), o.Normal));
			o.Emission *= 1 - ((1 - _RimColor.rgb) * pow (rim, _RimPower));
			o.Albedo = 0;
		}
		ENDCG
	}
	Fallback "Diffuse"
}