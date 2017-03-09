Shader "Curved/Curved Transparent" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_QOffset ("Offset", Vector) = (0,0,0,0)
		_Dist ("Distance", Float) = 100.0
		_Alpha ("Transparency", Float) = 0.5
	}
	
	SubShader {
		//Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		
		
		//BLENDING
		//Blending operations are limited by hardware
				
		//The source color (S) is the value written by the fragment shader. The destination color (D) is the color from the image in the framebuffer. 
				
		//BLENDING OPERATIONS:
		// By Default: The source and destination colors are added to each other. 
			//O = sS + dD. The The s and d are blending parameters that are multiplied into each of S and D before the addition.
				//BlendOp Min | Max | Sub | RevSub   ---  Instead of adding blended colors together, do a different operation on them:
				//Sub : Subtracts the destination from the source. O = sS - dD. The source and dest are again multiplied by blending parameters.
				//RevSub: Subtracts the source from the destination. O = sD - dS. The source and dest are multiplied by blending parameters.
				//Min: The output color is the component-wise minimum value of the source and dest colors. So performing GL_MIN in the RGB equation means that Or = min(Sr, Dr), Og = min(Sg, Dg), and so forth. The parameters s and d are ignored for this equation.
				//Max: The output color is the component-wise maximum value of the source and dest colors. The parameters s and d are ignored for this equation.
		
		
		//Blending PARAMETERS:
		
			//SINTAX:
			
		//Blend Off - Turn off blending
		//Blend SrcFactor DstFactor --> s*S + d*D
		//Blend SrcFactor DstFactor, SrcFactorA DstFactorA --> s*S + d*D &  - s*SAlpha + d*DAlpha

		//When blending is disabled to a buffer, the color from the fragment shader will be written directly to that buffer.
		//WARNING: Multiplying a color by itself, or its inverse, results in the blending operation being cancelled. 
		
			//PARAMETERS:	
			
		//One	The value of one - use this to let either the source or the destination color come through fully.
		//Zero	The value zero - use this to remove either the source or the destination values.
		
		//SrcColor	The value of this stage is multiplied by the source color value.
		//SrcAlpha	The value of this stage is multiplied by the source alpha value.
		
		//DstColor	The value of this stage is multiplied by frame buffer source color value.
		//DstAlpha	The value of this stage is multiplied by frame buffer source alpha value.
		
		//OneMinusSrcColor	The value of this stage is multiplied by (1 - source color).
		//OneMinusSrcAlpha	The value of this stage is multiplied by (1 - source alpha).
		
		//OneMinusDstColor	The value of this stage is multiplied by (1 - destination color).
		//OneMinusDstAlpha	The value of this stage is multiplied by (1 - destination alpha).
		
		//Blend Zero SrcColor
		
		
				
		
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

            sampler2D _MainTex;
			float4 _QOffset;
			float _Dist;
			half _Alpha;
			 
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    vPos += _QOffset*zOff*zOff;
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = v.texcoord;
			    return o;
			}

			half4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv.xy);
			    half outAlpha = _Alpha;
			    col.a = outAlpha;
			    return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
