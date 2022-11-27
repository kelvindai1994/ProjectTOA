// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:0,x:33638,y:32808,varname:node_0,prsc:2|diff-4850-OUT,spec-7154-OUT,gloss-144-OUT,normal-5555-RGB;n:type:ShaderForge.SFN_Slider,id:144,x:32990,y:33063,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:7154,x:33327,y:32974,varname:node_7154,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:5555,x:33308,y:33203,varname:node_5555,prsc:2,ntxv:0,isnm:False|TEX-2576-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2576,x:33147,y:33231,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:_Normal,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_RgbToHsv,id:7309,x:32656,y:32878,varname:node_7309,prsc:2|IN-9817-RGB;n:type:ShaderForge.SFN_HsvToRgb,id:9026,x:32837,y:32878,varname:node_9026,prsc:2|H-2951-HOUT,S-7309-SOUT,V-7309-VOUT;n:type:ShaderForge.SFN_Slider,id:9276,x:32088,y:33101,ptovrint:False,ptlb:HueShift,ptin:_HueShift,varname:_HueShift,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2797491,max:1;n:type:ShaderForge.SFN_Hue,id:8146,x:32475,y:33094,varname:node_8146,prsc:2|IN-9276-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:2951,x:32656,y:33094,varname:node_2951,prsc:2|IN-8146-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:4850,x:33051,y:32812,ptovrint:False,ptlb:HueShift Enabled?,ptin:_HueShiftEnabled,varname:_HueShiftEnabled,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-9817-RGB,B-9026-OUT;n:type:ShaderForge.SFN_Tex2d,id:9817,x:32319,y:32663,varname:BaseColor_Node,prsc:2,ntxv:0,isnm:False|TEX-9778-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9778,x:32041,y:32687,ptovrint:False,ptlb:BaseColor,ptin:_BaseColor,varname:_BaseColor,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:9778-2576-144-4850-9276;pass:END;sub:END;*/

Shader "S_MasterMat" {
    Properties {
        _BaseColor ("BaseColor", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Gloss ("Gloss", Range(0, 1)) = 1
        [MaterialToggle] _HueShiftEnabled ("HueShift Enabled?", Float ) = 0
        _HueShift ("HueShift", Range(0, 1)) = 0.2797491
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _BaseColor; uniform float4 _BaseColor_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueShift)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _HueShiftEnabled)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_5555 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = node_5555.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float perceptualRoughness = 1.0 - _Gloss_var;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 BaseColor_Node = tex2D(_BaseColor,TRANSFORM_TEX(i.uv0, _BaseColor));
                float _HueShift_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueShift );
                float3 node_8146 = saturate(3.0*abs(1.0-2.0*frac(_HueShift_var+float3(0.0,-1.0/3.0,1.0/3.0)))-1);
                float4 node_2951_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_2951_p = lerp(float4(float4(node_8146,0.0).zy, node_2951_k.wz), float4(float4(node_8146,0.0).yz, node_2951_k.xy), step(float4(node_8146,0.0).z, float4(node_8146,0.0).y));
                float4 node_2951_q = lerp(float4(node_2951_p.xyw, float4(node_8146,0.0).x), float4(float4(node_8146,0.0).x, node_2951_p.yzx), step(node_2951_p.x, float4(node_8146,0.0).x));
                float node_2951_d = node_2951_q.x - min(node_2951_q.w, node_2951_q.y);
                float node_2951_e = 1.0e-10;
                float3 node_2951 = float3(abs(node_2951_q.z + (node_2951_q.w - node_2951_q.y) / (6.0 * node_2951_d + node_2951_e)), node_2951_d / (node_2951_q.x + node_2951_e), node_2951_q.x);;
                float4 node_7309_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_7309_p = lerp(float4(float4(BaseColor_Node.rgb,0.0).zy, node_7309_k.wz), float4(float4(BaseColor_Node.rgb,0.0).yz, node_7309_k.xy), step(float4(BaseColor_Node.rgb,0.0).z, float4(BaseColor_Node.rgb,0.0).y));
                float4 node_7309_q = lerp(float4(node_7309_p.xyw, float4(BaseColor_Node.rgb,0.0).x), float4(float4(BaseColor_Node.rgb,0.0).x, node_7309_p.yzx), step(node_7309_p.x, float4(BaseColor_Node.rgb,0.0).x));
                float node_7309_d = node_7309_q.x - min(node_7309_q.w, node_7309_q.y);
                float node_7309_e = 1.0e-10;
                float3 node_7309 = float3(abs(node_7309_q.z + (node_7309_q.w - node_7309_q.y) / (6.0 * node_7309_d + node_7309_e)), node_7309_d / (node_7309_q.x + node_7309_e), node_7309_q.x);;
                float3 _HueShiftEnabled_var = lerp( BaseColor_Node.rgb, (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(node_2951.r+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_7309.g)*node_7309.b), UNITY_ACCESS_INSTANCED_PROP( Props, _HueShiftEnabled ) );
                float3 diffuseColor = _HueShiftEnabled_var; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _BaseColor; uniform float4 _BaseColor_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueShift)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _HueShiftEnabled)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_5555 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = node_5555.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float gloss = _Gloss_var;
                float perceptualRoughness = 1.0 - _Gloss_var;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 BaseColor_Node = tex2D(_BaseColor,TRANSFORM_TEX(i.uv0, _BaseColor));
                float _HueShift_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueShift );
                float3 node_8146 = saturate(3.0*abs(1.0-2.0*frac(_HueShift_var+float3(0.0,-1.0/3.0,1.0/3.0)))-1);
                float4 node_2951_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_2951_p = lerp(float4(float4(node_8146,0.0).zy, node_2951_k.wz), float4(float4(node_8146,0.0).yz, node_2951_k.xy), step(float4(node_8146,0.0).z, float4(node_8146,0.0).y));
                float4 node_2951_q = lerp(float4(node_2951_p.xyw, float4(node_8146,0.0).x), float4(float4(node_8146,0.0).x, node_2951_p.yzx), step(node_2951_p.x, float4(node_8146,0.0).x));
                float node_2951_d = node_2951_q.x - min(node_2951_q.w, node_2951_q.y);
                float node_2951_e = 1.0e-10;
                float3 node_2951 = float3(abs(node_2951_q.z + (node_2951_q.w - node_2951_q.y) / (6.0 * node_2951_d + node_2951_e)), node_2951_d / (node_2951_q.x + node_2951_e), node_2951_q.x);;
                float4 node_7309_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_7309_p = lerp(float4(float4(BaseColor_Node.rgb,0.0).zy, node_7309_k.wz), float4(float4(BaseColor_Node.rgb,0.0).yz, node_7309_k.xy), step(float4(BaseColor_Node.rgb,0.0).z, float4(BaseColor_Node.rgb,0.0).y));
                float4 node_7309_q = lerp(float4(node_7309_p.xyw, float4(BaseColor_Node.rgb,0.0).x), float4(float4(BaseColor_Node.rgb,0.0).x, node_7309_p.yzx), step(node_7309_p.x, float4(BaseColor_Node.rgb,0.0).x));
                float node_7309_d = node_7309_q.x - min(node_7309_q.w, node_7309_q.y);
                float node_7309_e = 1.0e-10;
                float3 node_7309 = float3(abs(node_7309_q.z + (node_7309_q.w - node_7309_q.y) / (6.0 * node_7309_d + node_7309_e)), node_7309_d / (node_7309_q.x + node_7309_e), node_7309_q.x);;
                float3 _HueShiftEnabled_var = lerp( BaseColor_Node.rgb, (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(node_2951.r+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_7309.g)*node_7309.b), UNITY_ACCESS_INSTANCED_PROP( Props, _HueShiftEnabled ) );
                float3 diffuseColor = _HueShiftEnabled_var; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _BaseColor; uniform float4 _BaseColor_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Gloss)
                UNITY_DEFINE_INSTANCED_PROP( float, _HueShift)
                UNITY_DEFINE_INSTANCED_PROP( fixed, _HueShiftEnabled)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 BaseColor_Node = tex2D(_BaseColor,TRANSFORM_TEX(i.uv0, _BaseColor));
                float _HueShift_var = UNITY_ACCESS_INSTANCED_PROP( Props, _HueShift );
                float3 node_8146 = saturate(3.0*abs(1.0-2.0*frac(_HueShift_var+float3(0.0,-1.0/3.0,1.0/3.0)))-1);
                float4 node_2951_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_2951_p = lerp(float4(float4(node_8146,0.0).zy, node_2951_k.wz), float4(float4(node_8146,0.0).yz, node_2951_k.xy), step(float4(node_8146,0.0).z, float4(node_8146,0.0).y));
                float4 node_2951_q = lerp(float4(node_2951_p.xyw, float4(node_8146,0.0).x), float4(float4(node_8146,0.0).x, node_2951_p.yzx), step(node_2951_p.x, float4(node_8146,0.0).x));
                float node_2951_d = node_2951_q.x - min(node_2951_q.w, node_2951_q.y);
                float node_2951_e = 1.0e-10;
                float3 node_2951 = float3(abs(node_2951_q.z + (node_2951_q.w - node_2951_q.y) / (6.0 * node_2951_d + node_2951_e)), node_2951_d / (node_2951_q.x + node_2951_e), node_2951_q.x);;
                float4 node_7309_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_7309_p = lerp(float4(float4(BaseColor_Node.rgb,0.0).zy, node_7309_k.wz), float4(float4(BaseColor_Node.rgb,0.0).yz, node_7309_k.xy), step(float4(BaseColor_Node.rgb,0.0).z, float4(BaseColor_Node.rgb,0.0).y));
                float4 node_7309_q = lerp(float4(node_7309_p.xyw, float4(BaseColor_Node.rgb,0.0).x), float4(float4(BaseColor_Node.rgb,0.0).x, node_7309_p.yzx), step(node_7309_p.x, float4(BaseColor_Node.rgb,0.0).x));
                float node_7309_d = node_7309_q.x - min(node_7309_q.w, node_7309_q.y);
                float node_7309_e = 1.0e-10;
                float3 node_7309 = float3(abs(node_7309_q.z + (node_7309_q.w - node_7309_q.y) / (6.0 * node_7309_d + node_7309_e)), node_7309_d / (node_7309_q.x + node_7309_e), node_7309_q.x);;
                float3 _HueShiftEnabled_var = lerp( BaseColor_Node.rgb, (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(node_2951.r+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_7309.g)*node_7309.b), UNITY_ACCESS_INSTANCED_PROP( Props, _HueShiftEnabled ) );
                float3 diffColor = _HueShiftEnabled_var;
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float _Gloss_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Gloss );
                float roughness = 1.0 - _Gloss_var;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
