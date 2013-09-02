Shader "Unlit Add 2 Textures" { 
	
Properties {
	_MainTex ("Texture 1", 2D) = "" 
	_Texture2 ("Texture 2", 2D) = ""
}

Category {
	SubShader {Pass {
		Lighting Off
		Cull Off
		SetTexture[_MainTex]
		SetTexture[_Texture2] { 
			Combine texture + previous
		}
		SetTexture[_] {Combine previous + primary}
	}}
}

}