// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GL3_ProjectAna_Holoshader"
{
	Properties
	{
		_CharacterTexture("Character Texture", 2D) = "gray" {}
		_CharacterNormal("Character Normal", 2D) = "bump" {}
		_NormalMaskStrength("Normal Mask Strength", Range( 0 , 2)) = 0.5
		_Opacity("Opacity", Range( 0 , 1)) = 0.75
		_CharacterColorTint("Character ColorTint", Color) = (1,1,1,1)
		_Noise_Texture("Noise_Texture", 2D) = "white" {}
		_Noise_Texture2("Noise_Texture2", 2D) = "white" {}
		_NoiseTextureZoom("NoiseTextureZoom", Range( 0 , 2)) = 1
		_NoiseTextureZoom2("NoiseTextureZoom 2", Range( 0 , 2)) = 0.1
		_NoiseSpeed("Noise Speed", Range( 0 , 3)) = 1
		_NoiseTexturesTransparency("NoiseTexture's Transparency", Range( 0 , 0.75)) = 0.33
		_MainNoiseTransparency("MainNoise Transparency", Range( 0 , 1)) = 0.3
		_ScanLineSpeed("ScanLine Speed", Range( -2 , 2)) = 0.4
		_ScanlineSegments("Scanline Segments", Range( -3 , 3)) = 2
		_ScanlinePower("Scanline Power", Range( 0 , 10)) = 3
		_ScanlineColor("Scanline Color", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		BlendOp Add
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _CharacterColorTint;
		uniform sampler2D _CharacterTexture;
		uniform float4 _CharacterTexture_ST;
		uniform sampler2D _CharacterNormal;
		uniform float4 _CharacterNormal_ST;
		uniform float _NormalMaskStrength;
		uniform sampler2D _Noise_Texture2;
		uniform float _NoiseTextureZoom;
		uniform float _NoiseTextureZoom2;
		uniform sampler2D _Noise_Texture;
		uniform float _MainNoiseTransparency;
		uniform float _NoiseSpeed;
		uniform float _NoiseTexturesTransparency;
		uniform float _ScanLineSpeed;
		uniform float _ScanlineSegments;
		uniform float _ScanlinePower;
		uniform float4 _ScanlineColor;
		uniform float _Opacity;


		float3 mod289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod289( i );
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


		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_CharacterTexture = i.uv_texcoord * _CharacterTexture_ST.xy + _CharacterTexture_ST.zw;
			float4 tex2DNode24 = tex2D( _CharacterTexture, uv_CharacterTexture );
			float2 uv_CharacterNormal = i.uv_texcoord * _CharacterNormal_ST.xy + _CharacterNormal_ST.zw;
			float4 lerpResult39 = lerp( tex2DNode24 , float4( 0,0,0,0 ) , Luminance(( UnpackNormal( tex2D( _CharacterNormal, uv_CharacterNormal ) ) * _NormalMaskStrength )));
			float2 uv_TexCoord111 = i.uv_texcoord * float2( -5,5 ) + float2( 0,0 );
			float2 panner60 = ( ( uv_TexCoord111 * _NoiseTextureZoom ) + 1.0 * _Time.y * float2( 0.02,0.15 ));
			float4 tex2DNode142 = tex2D( _Noise_Texture2, ( panner60 * _NoiseTextureZoom2 ) );
			float3 desaturateVar149 = lerp( tex2DNode142.rgb,dot(tex2DNode142.rgb,float3(0.299,0.587,0.114)).xxx,1.5);
			float temp_output_169_0 = frac( ( _NoiseSpeed * _Time.x ) );
			float2 uv_TexCoord161 = i.uv_texcoord * float2( 8,8 ) + float2( 0,0 );
			float div181=256.0/float(3);
			float4 posterize181 = ( floor( float4( ( round( ( 2 * 3 * _NoiseSpeed * temp_output_169_0 ) ) + uv_TexCoord161 ), 0.0 , 0.0 ) * div181 ) / div181 );
			float simplePerlin2D182 = snoise( posterize181.rg );
			float div176=256.0/float(3);
			float4 posterize176 = ( floor( float4( ( round( ( _NoiseSpeed * 3 * temp_output_169_0 ) ) + uv_TexCoord161 ), 0.0 , 0.0 ) * div176 ) / div176 );
			float simplePerlin2D177 = snoise( posterize176.rg );
			float clampResult193 = clamp( ( simplePerlin2D182 + simplePerlin2D177 ) , 1 , 1 );
			float div174=256.0/float(70);
			float4 posterize174 = ( floor( float4( ( round( ( temp_output_169_0 * 70 * _NoiseSpeed ) ) + uv_TexCoord161 ), 0.0 , 0.0 ) * div174 ) / div174 );
			float simplePerlin2D175 = snoise( posterize174.rg );
			float4 clampResult200 = clamp( ( ( float4( desaturateVar149 , 0.0 ) * tex2D( _Noise_Texture, panner60 ) ) + ( _MainNoiseTransparency * clampResult193 * simplePerlin2D175 ) + _NoiseTexturesTransparency ) , float4( 0,0,0,0 ) , float4( 0.022,0.016,0.203,1 ) );
			float2 uv_TexCoord210 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float4 clampResult168 = clamp( ( clampResult200 + ( pow( frac( ( ( uv_TexCoord210.y + ( _Time.y * _ScanLineSpeed ) ) * _ScanlineSegments ) ) , _ScanlinePower ) * _ScanlineColor ) ) , float4( 0,0,0,0 ) , float4( 0.1621972,0.5247585,0.8823529,0.641 ) );
			o.Emission = ( ( _CharacterColorTint * lerpResult39 ) + clampResult168 ).rgb;
			o.Alpha = ( tex2DNode24.a * _Opacity );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14001
47;60;1075;973;2655.979;471.7781;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;211;-2433.211,151.7851;Float;False;Property;_NoiseSpeed;Noise Speed;13;0;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;152;-2358.928,414.9897;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;155;-2088.858,334.8675;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;169;-1935.763,334.7109;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;186;-2128.371,105.9631;Float;False;Constant;_Int0;Int 0;23;0;2;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;189;-2129.709,26.56795;Float;False;Constant;_Int1;Int 1;23;0;3;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-1789.905,210.709;Float;False;3;3;0;FLOAT;0.0;False;1;INT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;184;-1788.087,67.48811;Float;False;4;4;0;INT;0;False;1;INT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;178;-1644.251,211.302;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;173;-2036.215,443.873;Float;False;Constant;_NoisePosterizeStrength;Noise Posterize Strength;23;0;70;0;1;INT;0
Node;AmplifyShaderEditor.RangedFloatNode;99;-2781.428,-218.5957;Float;False;Property;_NoiseTextureZoom;NoiseTextureZoom;11;0;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;161;-1761.173,452.1018;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;8,8;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RoundOpNode;183;-1636.65,68.63777;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;111;-2774.707,-349.6151;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;-5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-1792.241,334.3447;Float;False;3;3;0;FLOAT;0.0;False;1;INT;0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-2532.039,-331.5851;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;180;-1502.634,209.8783;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;190;-1407.287,22.23826;Float;False;1;0;INT;0;False;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;185;-1503.033,69.77071;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;191;-1429.752,30.13094;Float;False;1;0;INT;0;False;1;INT;0
Node;AmplifyShaderEditor.PannerNode;60;-2399.888,-328.8953;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.02,0.15;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-2497.873,-115.9303;Float;False;Property;_NoiseTextureZoom2;NoiseTextureZoom 2;12;0;0.1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;153;-2173.352,596.9409;Float;False;Property;_ScanLineSpeed;ScanLine Speed;17;0;0.4;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;170;-1644.055,333.9377;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;176;-1360.345,210.549;Float;False;2;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PosterizeNode;181;-1361.075,73.55464;Float;False;2;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;144;-2253.732,-217.5865;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;210;-1732.129,636.4871;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;177;-1200.614,210.6098;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;-1863.102,582.2734;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;182;-1185.231,69.94576;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;157;-1502.65,331.9577;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT2;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;213;-2125.979,-437.7781;Float;False;301;213;Cloud Txture here;1;58;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;164;-1506.439,583.9526;Float;False;Property;_ScanlineSegments;Scanline Segments;18;0;2;-3;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;192;-969.0736,153.458;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;159;-1393.534,476.1049;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;174;-1361.104,331.0715;Float;False;2;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;142;-2118.354,-206.4764;Float;True;Property;_Noise_Texture2;Noise_Texture2;10;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;-1226.937,474.6902;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;193;-888.7134,243.4954;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;199;-964.2821,75.15974;Float;False;Property;_MainNoiseTransparency;MainNoise Transparency;15;0;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;149;-1819.534,-226.2902;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1.5;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;58;-2110.854,-407.0385;Float;True;Property;_Noise_Texture;Noise_Texture;9;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;175;-1197.816,328.689;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;198;-726.1659,233.7998;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;166;-1164.617,586.3716;Float;False;Property;_ScanlinePower;Scanline Power;19;0;3;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-970.7149,-17.49759;Float;False;Property;_NoiseTexturesTransparency;NoiseTexture's Transparency;14;0;0.33;0;0.75;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-935.6504,-372.1426;Float;False;Property;_NormalMaskStrength;Normal Mask Strength;3;0;0.5;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;-1636.698,-281.6702;Float;False;2;2;0;FLOAT3;0,0,0,0;False;1;COLOR;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;25;-947.8167,-560.3503;Float;True;Property;_CharacterNormal;Character Normal;2;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;163;-1064.332,475.5775;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-619.8661,-485.7727;Float;False;2;2;0;FLOAT3;0,0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;209;-600.0625,113.5804;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;203;-881.4941,672.9423;Float;False;Property;_ScanlineColor;Scanline Color;20;0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;165;-859.8333,430.2884;Float;True;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;24;-747.67,-750.4404;Float;True;Property;_CharacterTexture;Character Texture;1;0;None;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;200;-581.2596,282.6851;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0.022,0.016,0.203,1;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCGrayscale;137;-636.9692,-557.7856;Float;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;204;-581.5248,526.2035;Float;True;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;201;-346.6677,540.2588;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;39;-440.5082,-580.3343;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;38;-440.3643,-746.2105;Float;False;Property;_CharacterColorTint;Character ColorTint;5;0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-197.168,-640.2435;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;131;-217.7144,89.81367;Float;False;Property;_Opacity;Opacity;4;0;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;127;-140.5744,-537.7733;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;168;-212.6767,452.4907;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0.1621972,0.5247585,0.8823529,0.641;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;42;-2378.848,-1023.108;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-1418.286,-726.1;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;94;-2370.291,-803.1394;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;86.8627,-36.82048;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;167;177.0857,123.0615;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;104;-2755.427,-1023.662;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;-1,-1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;87;1628.142,-966.7072;Float;True;Property;_Scanlines_T2;Scanlines_T2;16;0;Assets/Gamelab_Jaar3_TeamAbsence/HoloShader/TestFiles/Scanlines_T2.psd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-2500.52,-209.8105;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;51;-3520.218,1985.694;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.1;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-1821.964,-660.0114;Float;False;Constant;_GlowPower;GlowPower;9;0;0.5769206;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;50;-4025.638,1921.215;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-3961.026,2082.136;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;2,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-2510.485,-1022.551;Float;False;2;2;0;FLOAT2;0.0,0,0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-2784.465,-683.0098;Float;False;Constant;_Scanline_2;Scanline_2;6;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-2517.338,-803.7123;Float;False;2;2;0;FLOAT2;0.0,0,0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;55;-4291.815,2035.443;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;139;-1555.064,-658.4034;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;49;-3201.039,1889.012;Float;True;Property;_TextureSample1;Texture Sample 1;6;0;Assets/Gamelab_Jaar3_TeamAbsence/HoloShader/TestFiles/Scanlines_T.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-2199.177,-963.6411;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0.0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-3711.408,2030.286;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;26;-2057.555,-1037.905;Float;True;Property;_Scanlines_T1;Scanlines_T1;8;0;Assets/Gamelab_Jaar3_TeamAbsence/HoloShader/TestFiles/Scanlines_T.tga;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;124;-1750.251,-967.5433;Float;False;Constant;_Color1;Color 1;17;0;0,0.751724,1,0.666;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;105;-2765.854,-808.4862;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,-4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;81;-2470.783,-905.8523;Half;False;Constant;_Float3;Float 3;13;0;0.5;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-2195.329,-776.1927;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0.0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;96;-2059.5,-844.606;Float;True;Property;_TextureSample4;Texture Sample 4;7;0;Assets/Gamelab_Jaar3_TeamAbsence/HoloShader/TestFiles/Scanlines_T.tga;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-2753.023,-900.5864;Float;False;Constant;_Scanline_1;Scanline_1;7;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;147;-2771.524,-135.0832;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;-2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-1565.229,-767.2247;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-2463.539,-684.3801;Half;False;Constant;_Float7;Float 7;15;0;-1.737175;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-1739.448,-786.2407;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-4082.707,2198.964;Float;False;Constant;_Float1;Float 1;4;0;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;148;-1825.209,-110.2838;Float;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;28;371.1996,-129.6232;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;GL3_ProjectAna_Holoshader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Custom;0.5;True;False;0;True;TransparentCutout;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;Add;Add;0;False;0.19;0.6013192,0.8899624,0.9852941,0.684;VertexScale;True;False;Cylindrical;True;Relative;0;;0;-1;-1;-1;0;0;0;False;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;155;0;211;0
WireConnection;155;1;152;1
WireConnection;169;0;155;0
WireConnection;179;0;211;0
WireConnection;179;1;189;0
WireConnection;179;2;169;0
WireConnection;184;0;186;0
WireConnection;184;1;189;0
WireConnection;184;2;211;0
WireConnection;184;3;169;0
WireConnection;178;0;179;0
WireConnection;183;0;184;0
WireConnection;154;0;169;0
WireConnection;154;1;173;0
WireConnection;154;2;211;0
WireConnection;103;0;111;0
WireConnection;103;1;99;0
WireConnection;180;0;178;0
WireConnection;180;1;161;0
WireConnection;190;0;189;0
WireConnection;185;0;183;0
WireConnection;185;1;161;0
WireConnection;191;0;189;0
WireConnection;60;0;103;0
WireConnection;170;0;154;0
WireConnection;176;1;180;0
WireConnection;176;0;191;0
WireConnection;181;1;185;0
WireConnection;181;0;190;0
WireConnection;144;0;60;0
WireConnection;144;1;146;0
WireConnection;177;0;176;0
WireConnection;160;0;152;2
WireConnection;160;1;153;0
WireConnection;182;0;181;0
WireConnection;157;0;170;0
WireConnection;157;1;161;0
WireConnection;192;0;182;0
WireConnection;192;1;177;0
WireConnection;159;0;210;2
WireConnection;159;1;160;0
WireConnection;174;1;157;0
WireConnection;174;0;173;0
WireConnection;142;1;144;0
WireConnection;156;0;159;0
WireConnection;156;1;164;0
WireConnection;193;0;192;0
WireConnection;149;0;142;0
WireConnection;58;1;60;0
WireConnection;175;0;174;0
WireConnection;198;0;199;0
WireConnection;198;1;193;0
WireConnection;198;2;175;0
WireConnection;143;0;149;0
WireConnection;143;1;58;0
WireConnection;163;0;156;0
WireConnection;29;0;25;0
WireConnection;29;1;79;0
WireConnection;209;0;143;0
WireConnection;209;1;198;0
WireConnection;209;2;212;0
WireConnection;165;0;163;0
WireConnection;165;1;166;0
WireConnection;200;0;209;0
WireConnection;137;0;29;0
WireConnection;204;0;165;0
WireConnection;204;1;203;0
WireConnection;201;0;200;0
WireConnection;201;1;204;0
WireConnection;39;0;24;0
WireConnection;39;2;137;0
WireConnection;34;0;38;0
WireConnection;34;1;39;0
WireConnection;127;0;24;4
WireConnection;168;0;201;0
WireConnection;42;0;43;0
WireConnection;70;0;126;0
WireConnection;70;1;139;0
WireConnection;94;0;92;0
WireConnection;128;0;127;0
WireConnection;128;1;131;0
WireConnection;167;0;34;0
WireConnection;167;1;168;0
WireConnection;145;0;147;0
WireConnection;51;0;52;0
WireConnection;54;0;55;0
WireConnection;54;1;53;0
WireConnection;43;0;104;0
WireConnection;43;1;48;0
WireConnection;92;0;105;0
WireConnection;92;1;88;0
WireConnection;139;1;69;0
WireConnection;49;1;51;0
WireConnection;86;0;42;0
WireConnection;86;1;81;0
WireConnection;52;0;50;0
WireConnection;52;1;54;0
WireConnection;26;1;86;0
WireConnection;95;0;94;0
WireConnection;95;1;93;0
WireConnection;96;1;95;0
WireConnection;126;0;124;0
WireConnection;126;1;101;0
WireConnection;101;0;96;0
WireConnection;101;1;26;0
WireConnection;148;0;142;0
WireConnection;28;2;167;0
WireConnection;28;9;128;0
ASEEND*/
//CHKSM=7E1686C7D2C85C7D17633905C346F7A9604B302E