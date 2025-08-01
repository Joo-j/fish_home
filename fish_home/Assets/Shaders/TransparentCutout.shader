// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Transparent Cutout" {
    Properties {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Color ("Main color", Color) = (1,1,1,1)
    
        _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    
        _Stroke ("Stroke alpha", Range(0,1)) = 0.1
        _StrokeColor ("Stroke color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 100
    
        Lighting Off
    
        Pass {  
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                
                #include "UnityCG.cginc"
    
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };
    
                struct v2f {
                    float4 vertex : SV_POSITION;
                    half2 texcoord : TEXCOORD0;
                };
    
                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed _Cutoff;
    
                half4 _Color;
    
                fixed _Stroke;
                half4 _StrokeColor;
    
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }
                
                fixed4 frag (v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.texcoord);
                    clip(col.a - _Cutoff);
                    if (col.a < _Stroke) {
                        col = _StrokeColor;
                    } else {
                        col = col;
                    }
    
                    return col;
                }
            ENDCG
        }
    }
    
    }
    