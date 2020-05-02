Shader "Hidden/PPRLab"
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


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            uniform float _ParamA;
            uniform float _Start;
            uniform float _End;
            uniform float _XShad;
            uniform float _YShad;

			uniform float _ellipseHorizontalRadii;
			uniform float _ellipseVerticalRadii;
			uniform float _ellipseShadingOffset;
			uniform float2 _ellipsePosition;

			float ellipseTest(v2f i) 
			{
				float xPos = i.uv.x - _ellipsePosition.x;
				float yPos = i.uv.y - _ellipsePosition.y;

				xPos = xPos / _ellipseHorizontalRadii;
				yPos = yPos / _ellipseVerticalRadii;
				float ellipseTest = xPos*xPos + yPos*yPos;

				if (ellipseTest < 1)
				{
					//Nothing shall be changed - return 1 as multiplier for the color.
					return 1.0f;
				}
				else 
				{
					//Apply smooth change of shading where necessary. For further points -
					//return pure 0 to make the area black.
					return 1 - min((ellipseTest - 1) / _ellipseShadingOffset, 1);
				}
			}

            fixed4 frag(v2f i) : SV_Target
            {
				float ellipseValue = ellipseTest(i);

                float rdk = _ParamA;
                float start = _Start - 0.5;
                float end = _End - 0.5;
                float2 xy = i.uv -0.5f;
                rdk = rdk * sign(xy.x);
                if (xy.x>end ) {
                    float x = xy.x - abs(end);
                    xy.x = -((rdk * x * x) - xy.x);
                }
                else if (xy.x < start) {
                    float x = xy.x + abs(start);
                    xy.x = -((rdk * x * x) - xy.x);
                }
                xy += 0.5;
                fixed4 col = tex2D(_MainTex, xy);
                
                float X = 0.0f;
                float Y = 0.0f;
                if (i.uv.x < _XShad) {
                    X = (_XShad - i.uv.x) / _XShad;
                }
                else if (i.uv.x >(1- _XShad)) {
                    X = (i.uv.x - (1- _XShad))/ _XShad;
                }
                if (i.uv.y > (1- _YShad)) {
                    Y = (i.uv.y - (1 - _YShad))/ _YShad ;
                }
                else if (i.uv.y < _YShad) {
                    Y = (_YShad - i.uv.y)/_YShad;
                }

                float vin = 1- max(X,Y);

				vin = min(vin, ellipseValue);

                col *= vin;
                return col; 
            }
            ENDCG
        }
    }
}
