// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
 
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
 
Shader "Unlit/AlphaTestMat"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range(0, 1.0)) = 0.3
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Specular("Specular", Color) = (1.0, 1.0, 1.0, 1.0)
        _Gloss("Gloss", float) = 8.0
    }
    SubShader
    {
        Tags { 
            "RenderType"="AlphaTest"
            "IgnoreProjector"="True"
            "RenderType"="TransparentCutout"
        }
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "Lighting.cginc"
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Cutoff;
            half4 _Color;
            half4 _Specular;
            float _Gloss;
 
            struct a2v{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f{
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldLight : TEXCOORD2;
                float3 worldView : TEXCOORD3;
                float2 uv : TEXCOORD0;
            };
 
            v2f vert(a2v i){
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.vertex = UnityObjectToClipPos(i.vertex);
 
                float4 worldPos = mul(unity_ObjectToWorld, i.vertex);
                o.worldNormal = normalize(UnityObjectToWorldNormal(i.normal));
                o.worldLight = normalize(UnityWorldSpaceLightDir(worldPos));
                o.worldView = normalize(UnityWorldSpaceViewDir(worldPos));
 
                o.uv = TRANSFORM_TEX(i.texcoord, _MainTex);
                return o;
            }
 
            half4 frag(v2f i) : SV_TARGET{
                half4 meshColor = tex2D(_MainTex, i.uv);
 
                clip(meshColor.a - _Cutoff);
 
                half3 diffuse = _LightColor0 * meshColor.rgb * saturate(dot(i.worldNormal, i.worldLight));
 
                half3 ambient = UNITY_LIGHTMODEL_AMBIENT * _Color;
 
                half3 specular = _LightColor0 * _Specular * pow(saturate(dot(reflect(-i.worldLight, i.worldNormal), i.worldView)), _Gloss);
 
                return half4(diffuse + ambient + specular, 1.0);
            }
 
            ENDCG
        }
    }
}
