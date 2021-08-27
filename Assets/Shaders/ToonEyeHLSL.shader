Shader "CGCV/ToonEyeHLSL"
{
    Properties
    {
        _Depth   ("Depth"   , Range(0, 1  )) = 0.5
        _Gradient("Gradient", Range(0, 100)) = 20
        
        [Header(Iris)]
        _IrisBoundaryColor ("Iris Boundary Color" , Color      ) = (0, 0, 1, 1)
        _IrisBoundaryRadius("Iris Boundary Radius", Range(0, 1)) = 0.4
        _IrisInteriorColor ("Iris Interior Color" , Color      ) = (0, 1, 0, 1)
        _IrisInteriorRadius("Iris Interior Radius", Range(0, 1)) = 0.38
        
        [Header(Pupil)]
        _PupilBoundaryColor ("Pupil Boundary Color" , Color      ) = (0.13333, 0.13725, 0.13725, 1)
        _PupilBoundaryRadius("Pupil Boundary Radius", Range(0, 1)) = 0.2
        _PupilInteriorColor ("Pupil Interior Color" , Color      ) = (0.53725, 0.55294, 0.54118, 1)
        _PupilInteriorRadius("Pupil Interior Radius", Range(0, 1)) = 0.19
        
        [Header(Sclera)]
        _ScleraColor("Sclera Color", Color) = (0.94118, 0.96471, 0.94118, 1)
    }
    SubShader
    {   

        Tags {"RenderType" = "Cutoff" "RenderPipeline" = "UniversalPipeline"}

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float _Depth;
            float _Gradient;

            float4 _IrisBoundaryColor;
            float _IrisBoundaryRadius;
            float4 _IrisInteriorColor;
            float _IrisInteriorRadius;

            float4 _PupilBoundaryColor;
            float _PupilBoundaryRadius;
            float4 _PupilInteriorColor;
            float _PupilInteriorRadius;

            float4 _ScleraColor;
        CBUFFER_END

        struct Appdata
        {
            float4 vertex   : POSITION;  // The vertex position in model space.
            float3 normal   : NORMAL;    // The vertex normal in model space.
            float4 texcoord : TEXCOORD0; // The first UV coordinate.
        };

        struct v2f  
        {
            float4 illumination   : Color0;
            float4 clipPosition   : SV_POSITION;
            float3 objectPosition : TEXCOORD0;
        };

        ENDHLSL

        Pass
        {
            // Tags {"LightMode" = "ForwardBase"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            v2f vert (Appdata v)
            {
                v2f o;
                //half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half3 worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                float4 diffuse = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz)) * _LightColor0;
                float3 ambient = ShadeSH9(half4(worldNormal, 1));
                o.illumination = half4(ambient, 0) + diffuse;
                o.clipPosition = UnityObjectToClipPos(v.vertex.xyz);
                o.objectPosition = v.vertex.xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float mask = distance(0, float3(i.objectPosition.xy, i.objectPosition.z - _Depth));
                float irisBoundaryMask = saturate(_Gradient * (1 - saturate(mask / _IrisBoundaryRadius)));
                float irisInteriorMask = saturate(_Gradient * (1 - saturate(mask / _IrisInteriorRadius)));
                float pupilBoundaryMask = saturate(_Gradient * (1 - saturate(mask / _PupilBoundaryRadius)));
                float pupilInteriorMask = saturate(_Gradient * (1 - saturate(mask / _PupilInteriorRadius)));
                fixed4 irisInterior = (irisInteriorMask - pupilBoundaryMask) * lerp(_IrisInteriorColor, _IrisBoundaryColor, mask / _IrisInteriorRadius);
                fixed4 irisBoundary = (irisBoundaryMask - irisInteriorMask) * _IrisBoundaryColor;
                fixed4 pupilInterior = pupilInteriorMask * lerp(_PupilInteriorColor, _PupilBoundaryColor, mask / _PupilInteriorRadius);
                fixed4 pupilBoundary = (pupilBoundaryMask - pupilInteriorMask) * _PupilBoundaryColor;
                fixed4 sclera = (1 - irisBoundaryMask) *_ScleraColor;
                fixed4 color = (irisInterior + irisBoundary + pupilInterior + pupilBoundary + sclera) * i.illumination; 
                return color;
            }

            ENDHLSL
        }
    }
}
