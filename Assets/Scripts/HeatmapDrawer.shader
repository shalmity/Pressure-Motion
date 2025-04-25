Shader "Custom/HeatmapDrawer"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "black" {}
        _UV ("Draw UV", Vector) = (0, 0, 0, 0)
        _Strength ("Pressure Strength", Range(0,1)) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Blend One One

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _UV;
            float _Strength;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = distance(uv, _UV.xy);
                float falloff = saturate(1.0 - dist * 100.0); // 브러시 크기 조절
                return fixed4(_Strength * falloff, 0, 0, 1); // 빨간색으로 찍기
            }
            ENDCG
        }
    }
}
