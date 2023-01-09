// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "DiffSpecGloss"
{
	Properties 
	{
_MainColor("_MainColor", Color) = (1,1,1,1)
_Diffuse("_Diffuse", 2D) = "black" {}
_BumpMap("_BumpMap", 2D) = "black" {}
_GlossRange("_GlossRange", Range(0,1) ) = 0.5
_SpecularMapAlphaGloss("_SpecularMapAlphaGloss", 2D) = "black" {}
_CubeMapColor("_CubeMapColor", Color) = (1,1,1,1)
_CubeMapRange("_CubeMapRange", Range(0,1) ) = 0.5
_CubeMap("_CubeMap", Cube) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


float4 _MainColor;
sampler2D _Diffuse;
sampler2D _BumpMap;
float _GlossRange;
sampler2D _SpecularMapAlphaGloss;
float4 _CubeMapColor;
float _CubeMapRange;
samplerCUBE _CubeMap;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_Diffuse;
float3 simpleWorldRefl;
float2 uv_BumpMap;
float2 uv_SpecularMapAlphaGloss;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.simpleWorldRefl = -reflect( normalize(WorldSpaceViewDir(v.vertex)), normalize(mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL)));

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_Diffuse,(IN.uv_Diffuse.xyxy).xy);
float4 Multiply0=_MainColor * Tex2D0;
float4 Multiply3=_CubeMapRange.xxxx * _CubeMapColor;
float4 TexCUBE0=texCUBE(_CubeMap,float4( IN.simpleWorldRefl.x, IN.simpleWorldRefl.y,IN.simpleWorldRefl.z,1.0 ));
float4 Multiply2=Multiply3 * TexCUBE0;
float4 Add0=Multiply0 + Multiply2;
float4 Tex2DNormal0=float4(UnpackNormal( tex2D(_BumpMap,(IN.uv_BumpMap.xyxy).xy)).xyz, 1.0 );
float4 Tex2D3=tex2D(_SpecularMapAlphaGloss,(IN.uv_SpecularMapAlphaGloss.xyxy).xy);
float4 Multiply1=_GlossRange.xxxx * Tex2D3.aaaa;
float4 Tex2D1=tex2D(_SpecularMapAlphaGloss,(IN.uv_SpecularMapAlphaGloss.xyxy).xy);
float4 Master0_2_NoInput = float4(0,0,0,0);
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Add0;
o.Normal = Tex2DNormal0;
o.Specular = Multiply1;
o.Gloss = Tex2D1;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}