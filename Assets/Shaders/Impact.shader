Shader "Custom/Impact"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Brightness Threshold", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Threshold;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 col = tex2D(_MainTex, i.uv).rgb;
                float brightness = dot(col, float3(0.299, 0.587, 0.114));
                float bw = brightness > _Threshold ? 1.0 : 0.0;
                return float4(bw, bw, bw, 1.0);
            }
            ENDCG
        }
    }
}