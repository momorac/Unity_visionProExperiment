Shader "Custom/ShadowOnly"
{
    Properties
    {
        _ShadowOpacity("Shadow Opacity", Range(0,1)) = 0.55
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
        }
        LOD 100

        // 투명 알파 블렌드: 배경 위에 검은 알파만 얹음
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Pass
        {
            Name "Forward"
            Tags{ "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma target 2.0
            #pragma vertex   vert
            #pragma fragment frag

            // 메인 라이트 섀도우 전변형 지원(스크린/캐스케이드/맵)
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _SHADOWS_SOFT

            // XR / Instancing
            #pragma multi_compile_instancing
            #pragma prefer_hlslcc gles

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float _ShadowOpacity;
            CBUFFER_END

            struct Attributes
            {
                float3 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.positionWS = TransformObjectToWorld(IN.positionOS);
                OUT.positionCS = TransformWorldToHClip(OUT.positionWS);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                // 메인 라이트 + 섀도우 어텐(1=밝음, 0=그림자)
                #if defined(_MAIN_LIGHT_SHADOWS) || defined(_MAIN_LIGHT_SHADOWS_SCREEN)
                    float4 sc = TransformWorldToShadowCoord(IN.positionWS);
                    Light mainLight = GetMainLight(sc);
                #else
                    Light mainLight = GetMainLight();
                #endif

                float shadow = saturate(1.0 - mainLight.shadowAttenuation); // 0~1
                float a = saturate(shadow * _ShadowOpacity);

                // 검정 + 알파만 출력 → 배경을 어둡게
                return half4(0, 0, 0, a);
            }
            ENDHLSL
        }
    }

    FallBack Off
}