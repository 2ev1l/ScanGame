Shader "Custom/DepthOfField"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FocusPoint ("Focus Point", Vector) = (0.5,0.5,0,0)
        _FocusRange ("Focus Range", Range(0,1)) = 0.1
        _MaxBlur ("Max Blur", Range(0,0.1)) = 0.05
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _RotationMatrix ("Rotation Matrix", Vector) = (1,0,0,1)
    }

    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue"="Transparent"
            "PreviewType"="Plane" 
        }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _FocusPoint;
            float _FocusRange;
            float _MaxBlur;
            float _Smoothness;
            float4 _RotationMatrix;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 rotateUV(float2 uv, float4 rotMatrix)
            {
                return float2(
                    rotMatrix.x * uv.x + rotMatrix.y * uv.y,
                    rotMatrix.z * uv.x + rotMatrix.w * uv.y
                );
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distanceToFocus = length(i.uv - _FocusPoint);
                float blur = smoothstep(_FocusRange, _FocusRange + _Smoothness, distanceToFocus) * _MaxBlur;
                
                fixed4 original = tex2D(_MainTex, i.uv);
                if (blur <= 0.001)
                    return original;

                const int samples = 16;
                const float2 baseOffsets[16] = {
                    float2(-0.5, -0.5), float2(-0.5, 0.5),
                    float2(0.5, -0.5), float2(0.5, 0.5),
                    float2(-0.3, -0.3), float2(-0.3, 0.3),
                    float2(0.3, -0.3), float2(0.3, 0.3),
                    float2(-0.1, -0.1), float2(-0.1, 0.1),
                    float2(0.1, -0.1), float2(0.1, 0.1),
                    float2(0.0, -0.5), float2(0.0, 0.5),
                    float2(-0.5, 0.0), float2(0.5, 0.0)
                };

                fixed4 col = 0;
                for(int k = 0; k < samples; k++)
                {
                    float2 rotatedOffset = rotateUV(baseOffsets[k] * blur, _RotationMatrix);
                    col += tex2D(_MainTex, i.uv + rotatedOffset);
                }
                col /= samples;
                col.a = original.a; // Сохраняем оригинальную альфу
                
                return col;
            }
            ENDCG
        }
    }
}