Shader "Hidden/MRTSample"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

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

            struct psout
            {
                // SV_Target と COLOR は同じ意味を持ちます。
                // DirectX10 ~ は SV_Target を使っていますが, 
                // DirectX9 では COLOR を使っていました。
                // https://msdn.microsoft.com/ja-jp/library/bb509647(v=vs.85).aspx

                fixed4 colorR : SV_Target0;
                fixed4 colorG : SV_Target1;
                fixed4 colorB : SV_Target2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;

                return o;
            }
            
            sampler2D _MainTex;

            psout frag (v2f i)
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                psout output;
                output.colorR = fixed4(col.r, 0, 0, 1);
                output.colorG = fixed4(0, col.g, 0, 1);
                output.colorB = fixed4(0, 0, col.b, 1);
                return output;
            }

            ENDCG
        }
    }
}