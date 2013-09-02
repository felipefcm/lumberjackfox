Shader "Curved/Curved SoftAdd" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_QOffset ("Offset", Vector) = (0,0,0,0)
		_Dist ("Distance", Float) = 100.0
		_Soft ("Soft Factor", Float) = 2.0
	}
	
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdadd_fullshadows


            sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			float4 _QOffset;
			float _Dist;
			float _Soft;
			 
			struct appdata {
          		float4 vertex : POSITION;
          		float4 texcoord : TEXCOORD0;
            };
			 
			struct v2f {
          		float4 pos : POSITION;
          		float2 uv_MainTex: TEXCOORD0;
			};

			v2f vert (appdata v)
			{
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    vPos += _QOffset*zOff*zOff;
			    
			    //o.uv = v.texcoord;
			    // o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
          		o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);	
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    return o;
			}

			float4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv_MainTex)/_Soft;
			    return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
