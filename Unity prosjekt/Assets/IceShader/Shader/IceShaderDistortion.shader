Shader "Effects/Ice/IceDistortion" {
Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
        _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
		_ReflectionStrength ("ReflectionStrength", Range (1, 20)) = 1
        _MainTex ("Base (RGB) Emission Tex (A)", 2D) = "white" {}
		_Opacity ("Material opacity", Range (-1, 1)) = 0.5
        _Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _FPOW("FPOW Fresnel", Float) = 5.0
        _R0("R0 Fresnel", Float) = 0.05
		_Cutoff ("Cutoff", Range (0, 1)) = 0.5
		_LightStr ("Light strength", Range (0, 1)) = 1
		_BumpAmt ("Distortion", range (0,500)) = 10
}

SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transperent" }
		GrabPass {}		
        LOD 200
		ZWrite On

CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
#pragma vertex vert
#pragma surface surf Lambert alpha
#pragma target 3.0
#pragma glsl


sampler2D _MainTex;
sampler2D _BumpMap;
samplerCUBE _Cube;

float _BumpAmt;
sampler2D _GrabTexture;
float4 _GrabTexture_TexelSize;

float4 _Color;
float4 _ReflectColor;
float _ReflectionStrength;
float _Shininess;
float _FPOW;
float _R0;
float _Opacity;
float _Cutoff;
float _LightStr;

struct Input {
        float2 uv_MainTex;
        float2 uv_BumpMap;
        float3 viewDir;
        float3 worldRefl;
        INTERNAL_DATA
		float2 uv_BumpMapGlass;
	float4 proj : TEXCOORD0;
};

void vert (inout appdata_full v, out Input o) {
	float4 oPos = mul(UNITY_MATRIX_MVP, v.vertex);
	#if UNITY_UV_STARTS_AT_TOP
		float scale = -1.0;
	#else
		float scale = 1.0;
	#endif
	o.proj.xy = (float2(oPos.x, oPos.y*scale) + oPos.w) * 0.5;
	o.proj.zw = oPos.zw;
}

 
void surf (Input IN, inout SurfaceOutput o) {
        half4 tex = tex2D(_MainTex, IN.uv_MainTex);
        half4 c = tex * _Color;
        
       
        o.Gloss = tex.a;
        o.Specular = _Shininess;
       
        o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
       
        float3 worldRefl = WorldReflectionVector (IN, o.Normal);

        half4 reflcol = texCUBE (_Cube, worldRefl);
        reflcol *= tex.a;     

        half fresnel = saturate(1.0 - dot(o.Normal, normalize(IN.viewDir)));
        fresnel = pow(fresnel, _FPOW);
        fresnel = _R0 + (1.0 - _R0) * fresnel;
        reflcol = lerp(c, reflcol, fresnel);

	    half2 offset = o.Normal.rg * _BumpAmt * _GrabTexture_TexelSize.xy;
		IN.proj.xy = offset * IN.proj.z + IN.proj.xy;
		half4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.proj));

        //o.Emission = reflcol.rgb * _ReflectColor.rgb * _ReflectionStrength * _LightStr;
		o.Emission = (col.rgb + reflcol.rgb* _ReflectionStrength * _LightStr) * _ReflectColor.rgb;
		o.Albedo = _Color ;
        o.Alpha = _Cutoff > tex.a ? tex.a+_Opacity : 0;
}
ENDCG
}

FallBack "Reflective/Bumped Diffuse"
}