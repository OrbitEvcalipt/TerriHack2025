Shader "Custom/URP/BorderedLines"
{
    Properties
    {
        _LineColor  ("Line Color", Color) = (0,1,1,1)
        _LineWidth  ("Line Width (UV)", Range(0.001,0.2)) = 0.02

        _MainTex    ("Dot Texture", 2D) = "white" {}
        _DotTint    ("Dot Tint", Color) = (1,1,1,1)

        _TilePerUnitY ("Tiles per Unit (Y)", Float) = 1.0
        _MeshLengthY  ("Mesh base length Y (set 1 for Quad, 10 for Plane)", Float) = 1.0

        _Opacity    ("Opacity", Range(0,1)) = 1.0
        _ScrollY    ("Scroll Y (offset)", Float) = 0.0
        _ScrollSpeedY    ("_ScrollSpeed Y (offset)", Float) = 0.0
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Name "FORWARD"
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0; // uv.x = 0..1 for width; uv.y = tiled
            };

            sampler2D _MainTex;
            float4   _MainTex_ST;

            float4 _LineColor;
            float  _LineWidth;
            float4 _DotTint;
            float  _TilePerUnitY;
            float  _MeshLengthY;
            float  _Opacity;
            float  _ScrollY;
            float _ScrollSpeedY;

            // Vertex
            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);

                // X: обычный uv (0..1) — нужен для линий (края)
                float ux = v.uv.x;

                // Определяем реальный scale по оси Y из матрицы (учитывает rotate+scale)
                // берём колонку Y из unity_ObjectToWorld: mul(unity_ObjectToWorld, float4(0,1,0,0)).xyz
                float3 worldY = mul(unity_ObjectToWorld, float4(0,1,0,0)).xyz;
                float objectScaleY = length(worldY);

                // Количество повторов по Y = (scaleY / MeshBaseLengthY) * TilesPerUnitY
                float repeatsY = (objectScaleY / max(0.00001, _MeshLengthY)) * _TilePerUnitY;

                // Формируем UV для сэмпла: X — 0..1, Y — тайлится автоматически по размеру объекта
                float uy = v.uv.y * repeatsY + _ScrollY + _Time.y * _ScrollSpeedY;

                o.uv = float2(ux, uy);
                return o;
            }

            // Fragment
            float4 frag(Varyings IN) : SV_Target
            {
                // uv.x in 0..1 (width), uv.y may exceed 1.0 (tile)
                float2 uvSample = IN.uv;

                // линиии по краям (жёсткая маска; можно заменить на smoothstep для мягких краёв)
                float leftLine  = 1.0 - step(_LineWidth, uvSample.x);          // 1 если uv.x < _LineWidth
                float rightLine = step(1.0 - _LineWidth, uvSample.x);         // 1 если uv.x >= 1-_LineWidth
                float lineMask  = saturate(leftLine + rightLine);

                // семплим текстуру: X = uv.x (растянута 1 раз), Y = tiled based on object scale
                float2 texUV = float2(uvSample.x, uvSample.y);
                float4 tx = tex2D(_MainTex, texUV) * _DotTint;

                // итоговый цвет: линии перекрывают текстуру по маске
                float3 rgb = tx.rgb;
                rgb = lerp(rgb, _LineColor.rgb, lineMask);

                // итоговая альфа: текстурная альфа или линия
                float alpha = max(tx.a * _DotTint.a, lineMask * _LineColor.a);
                alpha *= _Opacity;

                return float4(rgb, alpha);
            }

            ENDHLSL
        }
    }
    FallBack "Unlit/Transparent"
}
