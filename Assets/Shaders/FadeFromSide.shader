Shader "Custom/NonLinearFadeBetweenTextures"
{
    Properties
    {
        _MainTex ("Texture 1", 2D) = "white" {}
        _SecondTex ("Texture 2", 2D) = "black" {}
        _Color ("Texture Tint", Color) = (1, 1, 1, 1)
        _FadeOffset ("Fade Offset", Range(0,1)) = 0.0
        _FadeWidth ("Fade Width", Range(0.01,1)) = 0.2
        _CurvePower ("Fade Curve Power", Range(1, 10)) = 2.0  // New property to control the non-linear transition.
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
            float _FadeOffset;
            float _FadeWidth;
            float _CurvePower;  // New variable to control the non-linear curve.

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
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);         // Transform the vertex position from object space to clip space.
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);           // Apply the texture scale and offset to the UV coordinates.

                // Normalize the X position from [-5, 5] to [0, 1] based on the default Unity plane (for a standard plane mesh).
                o.fadeCoord = (v.vertex.x + 5.0) / 10.0;       // This creates a normalized fade coordinate.

                return o;
            }

            // Fragment shader function.
            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate the fade amount based on normalized position.
                // This calculates how far the current fragment is from the fade offset.
                float fadeProgress = (i.fadeCoord - _FadeOffset) / _FadeWidth;

                // Apply a non-linear curve to the fade amount (pow or smoothstep can be used here).
                // Using pow to control the curve. The higher _CurvePower, the sharper the transition at the middle.
                float fade = pow(saturate(fadeProgress), _CurvePower);

                // Sample both textures.
                fixed4 tex1 = tex2D(_MainTex, i.uv);           // Sample the first texture (e.g., dirt texture).
                fixed4 tex2 = tex2D(_SecondTex, i.uv);         // Sample the second texture (e.g., grass texture).

                // Blend the two textures based on the fade amount. 
                // When fade is 0, tex1 is fully visible; when fade is 1, tex2 is fully visible.
                fixed4 blendedColor = lerp(tex1, tex2, fade);

                // Apply the tint color to the blended texture result.
                blendedColor *= _Color;

                // Return the final color after blending and tinting.
                return blendedColor;
            }

            ENDCG
        }
    }
    
    // Fallback shader if this shader is unsupported
    FallBack "Diffuse"
}
