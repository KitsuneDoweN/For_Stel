Shader "SFS/VoxelTree"
{
	Properties
	{
		[Header(BASE PROPERTIES)]
		[Space(5)]
		[NoScaleOffset]_MainTex("Albedo Texture", 2D) = "white" {}
		_Color("Texture Tint", Color) = (1,1,1,1)
		_TextureContrast("Texture Contrast", Range( 0 , 2)) = 1.3

		[Header(WIND)]
		[Space(5)]
		_BendAmount1("Tree Bend Amount", Range( 0 , 0.02)) = 0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _BendAmount1;
		uniform float _TextureContrast;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color;


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_92_0 = ( ( cos( ( ( ( ase_worldPos.x + ase_worldPos.z ) * 0.06 ) + _Time.y ) ) * ase_vertex3Pos.y ) * _BendAmount1 );
			float4 appendResult93 = (float4(temp_output_92_0 , 0.0 , temp_output_92_0 , 0.0));
			float4 break96 = mul( appendResult93, unity_ObjectToWorld );
			float4 appendResult98 = (float4(break96.x , 0 , break96.z , 0.0));
			float3 rotatedValue100 = RotateAroundAxis( float3( 0,0,0 ), appendResult98.xyz, float3( 0,0,0 ), 0.0 );
			v.vertex.xyz += rotatedValue100;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			o.Albedo = CalculateContrast(_TextureContrast,( tex2D( _MainTex, uv_MainTex) * _Color )).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}