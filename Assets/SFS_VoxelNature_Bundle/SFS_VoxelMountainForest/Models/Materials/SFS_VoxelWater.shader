Shader "SFS/VoxelWater"
{
	Properties
	{
		[Header(BASE PROPERTIES)]
		[Space(5)]
		_Metallic("Metallic", Range(0 , 1)) = 0
		_Glossiness("Smoothness", Range( 0 , 1)) = 0
		_LightColor("Light Color", Color) = (1,1,1,0)
		_DeepColor("Deep Color", Color) = (0,0.3608235,0.3882353,0)
		_WaterDepth("Water Depth", Range( 0 , 5)) = 1
		_Refraction("Refraction", Range( 0 , 0.2)) = 0.1

		[Header(WAVE PARAMETERS)]
		[Space(5)]
		_WaveScale("Wave Scale", Range( 0 , 1)) = 0.1
		_WaveSpeed("Wave Speed", Range( 0 , 0.3)) = 0.1
		_WaveHeight("Wave Height", Range( 0 , 10)) = 1
		_WaveDirection("Wave Direction", Vector) = (1,1,0,0)

		[Header(FOAM PARAMETERS)]
		[Space(5)]
		_WaterFoam("Water Foam", 2D) = "white" {}
		_WaveFoamTiling("Wave Foam Tiling", Range( 0.1 , 10)) = 2
		_WaveFoamSpeed("Wave Foam Speed", Range( 0 , 0.2)) = 0.01
		_WaveFoamOpacity("Wave Foam Opacity", Range( 0 , 1)) = 0.5

		[Header(NORMALS PARAMETERS)]
		[Space(5)]
		_NormalMap("Normal Map", 2D) = "white" {}
		_NormalScale("Normal Scale", Range( 0 , 3)) = 0.24
		_NormalSpeed("Normal Speed", Range( 0 , 1)) = 0.01
		_NormalStrength("Normal Strength", Range( 0 , 1)) = 0.3
		_NormalMainDirection("Normal Main Direction", Vector) = (1,0,0,0)
		_NormalSecondDirection("Normal Second Direction", Vector) = (-1,0,0,0)

		[Header(COLLISION PARAMETERS)]
		[Space(5)]
		_EdgeSize("Edge Size", Range( 0 , 1.5)) = 0
		_EdgePower("Edge Power", Range( 0 , 10)) = 1
		_EdgeColor("Edge Color", Color) = (1,1,1,0)
		_EdgeFoamSize("Edge Foam Size", Range( 0 , 2)) = 1.5
		_EdgeFoamOpacity("Edge Foam Opacity", Range( 0 , 1)) = 0.75
		_EdgeFoamFade("Edge Foam Fade", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
		};

		uniform float _WaveHeight;
		uniform float _WaveSpeed;
		uniform float2 _WaveDirection;
		uniform float _WaveScale;
		uniform sampler2D _NormalMap;
		uniform float2 _NormalMainDirection;
		uniform float _NormalSpeed;
		uniform float _NormalScale;
		uniform float _NormalStrength;
		uniform float2 _NormalSecondDirection;
		uniform float4 _DeepColor;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _Refraction;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _WaterDepth;
		uniform float4 _LightColor;
		uniform sampler2D _WaterFoam;
		uniform float _WaveFoamTiling;
		uniform float _WaveFoamSpeed;
		uniform float _WaveFoamOpacity;
		uniform float _EdgePower;
		uniform float4 _EdgeColor;
		uniform float _EdgeSize;
		uniform float _EdgeFoamOpacity;
		uniform float _EdgeFoamSize;
		uniform float _EdgeFoamFade;
		uniform float _Metallic;
		uniform float _Glossiness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 Tesselation131 = UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, 0.0,80.0,( _WaveHeight * 8.0 ));
			return Tesselation131;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult13 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldSpaceUV14 = appendResult13;
			float4 WaveTiling25 = ( ( WorldSpaceUV14 * float4( float2( 0.2,0.2 ), 0.0 , 0.0 ) ) * _WaveScale );
			float2 panner3 = ( ( _Time.y * _WaveSpeed ) * _WaveDirection + WaveTiling25.xy);
			float simplePerlin2D1 = snoise( panner3 );
			float3 WaveHeight37 = ( ( float3(0,0.1,0) * _WaveHeight ) * simplePerlin2D1 );
			v.vertex.xyz += WaveHeight37;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float4 appendResult13 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldSpaceUV14 = appendResult13;
			float4 temp_output_64_0 = ( WorldSpaceUV14 * _NormalScale );
			float2 panner68 = ( 1.0 * _Time.y * ( _NormalMainDirection * _NormalSpeed ) + temp_output_64_0.xy);
			float2 panner69 = ( 1.0 * _Time.y * ( _NormalSecondDirection * ( _NormalSpeed * 3.0 ) ) + ( temp_output_64_0 * ( _NormalScale * 5.0 ) ).xy);
			float3 NormalMap78 = BlendNormals( UnpackScaleNormal( tex2D( _NormalMap, panner68 ), _NormalStrength ) , UnpackScaleNormal( tex2D( _NormalMap, panner69 ), _NormalStrength ) );
			o.Normal = NormalMap78;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor117 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float3( (ase_grabScreenPosNorm).xy ,  0.0 ) + ( _Refraction * NormalMap78 ) ).xy);
			float4 clampResult118 = clamp( screenColor117 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Refraction120 = clampResult118;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth314 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float4 ase_vertex4Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 ase_viewPos = UnityObjectToViewPos( ase_vertex4Pos );
			float ase_screenDepth = -ase_viewPos.z;
			float temp_output_316_0 = ( eyeDepth314 - ase_screenDepth );
			float WaterDepth470 = _WaterDepth;
			float PerspectiveDepthMask477 = ( 1.0 - saturate( ( ( temp_output_316_0 * 0.2 ) + (0.0 + (( 1.0 - WaterDepth470 ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) ) ) );
			float screenDepth122 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth122 = abs( ( screenDepth122 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( ( _WaterDepth / 500.0 ) ) );
			float clampResult124 = clamp( ( 1.0 - distanceDepth122 ) , 0.0 , 1.0 );
			float OrthographicDepth125 = clampResult124;
			float temp_output_603_0 = ( unity_OrthoParams.w == 0.0 ? PerspectiveDepthMask477 : OrthographicDepth125 );
			float4 lerpResult606 = lerp( _DeepColor , Refraction120 , temp_output_603_0);
			float4 color624 = IsGammaSpace() ? float4(0.764151,0.764151,0.764151,0) : float4(0.5448383,0.5448383,0.5448383,0);
			float4 lerpResult611 = lerp( _LightColor , ( _DeepColor * color624 ) , ( 1.0 - temp_output_603_0 ));
			float2 panner142 = ( 1.0 * _Time.y * float2( 0,0 ) + ( ( WorldSpaceUV14 / 10.0 ) * _WaveFoamTiling ).xy);
			float2 temp_cast_5 = (_WaveFoamSpeed).xx;
			float2 panner104 = ( 1.0 * _Time.y * temp_cast_5 + ( WorldSpaceUV14 * 0.05 ).xy);
			float simplePerlin2D103 = snoise( panner104*0.9 );
			float4 WaveTiling25 = ( ( WorldSpaceUV14 * float4( float2( 0.2,0.2 ), 0.0 , 0.0 ) ) * _WaveScale );
			float2 panner3 = ( ( _Time.y * _WaveSpeed ) * _WaveDirection + WaveTiling25.xy);
			float simplePerlin2D1 = snoise( panner3 );
			float WavePatern519 = simplePerlin2D1;
			float clampResult109 = clamp( ( tex2D( _WaterFoam, panner142 ).r * ( simplePerlin2D103 + WavePatern519 + ( _WaveFoamSpeed * -1.0 ) ) ) , 0.0 , 1.0 );
			float4 temp_cast_9 = (clampResult109).xxxx;
			float4 color238 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float4 lerpResult239 = lerp( temp_cast_9 , color238 , ( 1.0 - _WaveFoamOpacity ));
			float4 WaveFoam100 = lerpResult239;
			float4 Albedo520 = ( ( lerpResult606 * lerpResult611 ) + ( WaveFoam100 * ( 1.0 - WavePatern519 ) ) );
			o.Albedo = Albedo520.rgb;
			float4 color463 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float EdgeSize333 = _EdgeSize;
			float PerspectiveEdgeMask327 = ( 1.0 - saturate( ( temp_output_316_0 + (0.0 + (( 1.0 - EdgeSize333 ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) ) ) );
			float4 lerpResult464 = lerp( _EdgeColor , color463 , ( 1.0 - PerspectiveEdgeMask327 ));
			float2 temp_cast_11 = (_WaveFoamSpeed).xx;
			float2 panner138 = ( 1.0 * _Time.y * temp_cast_11 + ( ( WorldSpaceUV14 / 10.0 ) * _WaveFoamTiling ).xy);
			float4 tex2DNode82 = tex2D( _WaterFoam, panner138 );
			float4 color173 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float EdgeFoamSize422 = _EdgeFoamSize;
			float PerspectiveFoamMask328 = ( 1.0 - saturate( ( temp_output_316_0 + (0.0 + (( 1.0 - EdgeFoamSize422 ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) ) ) );
			float4 lerpResult421 = lerp( ( _EdgeFoamOpacity * tex2DNode82 ) , color173 , ( 1.0 - PerspectiveFoamMask328 ));
			float4 color430 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float EdgeFoamFade415 = _EdgeFoamFade;
			float PerspectiveFoamFade433 = ( 1.0 - saturate( ( temp_output_316_0 + (0.0 + (( 1.0 - EdgeFoamFade415 ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) ) ) );
			float clampResult429 = clamp( ( ( 1.0 - PerspectiveFoamFade433 ) * ( 1.0 - 0.0 ) ) , 0.0 , 1.0 );
			float4 lerpResult431 = lerp( lerpResult421 , color430 , ( 1.0 - clampResult429 ));
			float4 PerspectiveWaterCollision376 = ( ( _EdgePower * lerpResult464 ) + lerpResult431 );
			float screenDepth181 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth181 = abs( ( screenDepth181 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( ( _EdgeSize / 5000.0 ) ) );
			float clampResult185 = clamp( ( ( 1.0 - distanceDepth181 ) * _EdgePower ) , 0.0 , 1.0 );
			float screenDepth51 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth51 = abs( ( screenDepth51 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( ( _EdgeFoamSize / 3000.0 ) ) );
			float4 lerpResult168 = lerp( ( _EdgeFoamOpacity * tex2DNode82 ) , color173 , distanceDepth51);
			float4 clampResult58 = clamp( lerpResult168 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 color234 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float screenDepth229 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth229 = abs( ( screenDepth229 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( ( _EdgeFoamFade / 5000.0 ) ) );
			float clampResult233 = clamp( ( ( 1.0 - distanceDepth229 ) * ( 1.0 - 0.0 ) ) , 0.0 , 1.0 );
			float4 lerpResult204 = lerp( clampResult58 , color234 , clampResult233);
			float4 OrthographicWaterCollision56 = ( ( clampResult185 * _EdgeColor ) + lerpResult204 );
			o.Emission = ( unity_OrthoParams.w == 0.0 ? PerspectiveWaterCollision376 : OrthographicWaterCollision56 ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}

		ENDCG
	}
}