Shader "Custom/FadeFromSide"
{
    Properties
    {
        _MainTex ("Texture 1", 2D) = "white" {}
        _SecondTex ("Texture 2", 2D) = "black" {}
        _Color ("Texture Tint", Color) = (1, 1, 1, 1)
        _FadeProgress ("Fade Progress", Range(0,1)) = 0.0
        _FadeWidth ("Fade Width", Range(0.001,1)) = 0.2
        _CurvePower ("Fade Curve Power", Range(1, 30)) = 2.0 
        _BumpSize ("Bump Size", Range(0.01, 2)) = 1.0
        _BumpWidth ("Bump Width", Range(0.01, 100)) = 1.0
        _ZOffset ("Z Offset", Float) = 1.0
        _FadeOffset ("Fade Offset", Range(-1,1)) = -0.11
        }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            // Transparency settings
            Tags { "Queue" = "Background" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest LEqual
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _SecondTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _FadeProgress;
            float _FadeWidth;
            float _CurvePower;
            float _BumpSize;
            float _BumpWidth;
            float _ZOffset;
            float _FadeOffset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float fadeCoord : TEXCOORD1;
                float localZ : TEXCOORD2;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);         // Transform the vertex position from object space to clip space.
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);           // Apply the texture scale and offset to the UV coordinates.

                // Normalize the X position from [-5, 5] to [0, 1] based on the default Unity plane (for a standard plane mesh).
                o.fadeCoord = (v.vertex.x + 5.0) / 10.0;       // This creates a normalized fade coordinate.

                o.localZ = v.vertex.z;

                return o;
            }

            // Fragment shader function.
            fixed4 frag(v2f i) : SV_Target
            {
                // Offset varies based on local Y position
                float dynamicOffset = _FadeProgress + _FadeOffset + sin((i.localZ)/_BumpWidth+_ZOffset)*_BumpSize;

                float fadeProgress = (i.fadeCoord - dynamicOffset) / _FadeWidth;
                float fade = pow(saturate(fadeProgress), _CurvePower);

                fixed4 tex1 = tex2D(_MainTex, i.uv);
                fixed4 tex2 = tex2D(_SecondTex, i.uv);
                fixed4 blendedColor = lerp(tex1, tex2, fade);

                blendedColor *= _Color;
                return blendedColor;
            }

            ENDCG
        }
    }
    
    // Fallback shader if this shader is unsupported
    FallBack "Diffuse"
}
