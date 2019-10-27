Shader "Custom/ColorRamp"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

	_MainTex ("Texture", 2D) = "white" {}
	_RampTex ("Ramp Texture", 2D) = "white" {}
	_Speed ("Speed", Range(1, 10)) = 1
	_GeometricSpeed("GeometricSpeed", Range(1, 10)) = 1

	_WaveFrequency ("WaveFrequency", Float) = 1
	_WaveAmplitude ("WaveAmplitude", Float) = 1
	_WaveHeight ("WaveHeight", Float) = 1
	_NoiseTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        #pragma target 3.0

        sampler2D _MainTex;
	sampler2D _RampTex;
	float _Speed;
	float _GeometricSpeed;

	float _WaveFrequency;
	float _WaveAmplitude;
	float _WaveHeight;
	sampler2D _NoiseTex;
	float4 _NoiseTex_ST;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

	void vert(inout appdata_full vertexData) {
	    float3 p = vertexData.vertex.xyz;
	    float noise = tex2Dlod(_NoiseTex, float4(vertexData.texcoord.xy, 0, 0));
	    p.y += (sin(_Time * _WaveFrequency * noise) - 0.5) * _WaveAmplitude + _WaveHeight;
	    vertexData.vertex.xyz = p;
	}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
	    // Color variation using ramp texture
	    fixed2 scroll = IN.uv_MainTex;
	    scroll += fixed2(_GeometricSpeed * _Time.x, 0);
	    half4 c = tex2D(_MainTex, scroll);
	    o.Albedo = tex2D(_RampTex, fixed2(c.r + _Time.x * _Speed, 0.5)).rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
