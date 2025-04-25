Shader "Custom/FloorShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Heatmap ("Heatmap", 2D) = "black" {}
    }

    SubShader
    {
        Tags { "Queue"="Geometry" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _Heatmap;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 baseColor = tex2D(_MainTex, i.uv);
                float4 heatColor = tex2D(_Heatmap, i.uv);
                return baseColor + heatColor;
            }
            ENDCG
        }
    }
}