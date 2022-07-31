#if OPENGL
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

extern float Time;
extern float4 Color;
extern float2 Resolution;
extern float4 ColorAddition;
sampler TextureSampler : register(s0);

struct PixelInput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float4 TexCoord : TEXCOORD0;
};

float MultiSin(float input, int it)
{
	float a = 0;
	
	for (int i = 0; i < it; i++)
	{
		a += sin(input * (i + 1)) * sin(input / (i + 1));
	}
	
	return a;
}

float Bubble(float4 coords, float2 timeMult, float2 offSet, float2 size, float threshold, float maximum)
{
	float output = (MultiSin((coords[0] * radians(720.0) + Time * timeMult[0] + radians(offSet[0])) * size[0], 2) * MultiSin((coords[1] * radians(720.0) + Time * timeMult[1] + radians(offSet[1])) * size[1], 2)) / 4.0;
	
	/*if (output > threshold)
		return maximum;*/
	
	return abs(output);
}

float4 PPEdit(float4 coords)
{
	float4 output = float4(0, 0, coords[2], coords[3]);
	float scale = 64.0;
	
	output[0] = (floor((coords[0] * scale) / 2) * 2) / scale;
	output[1] = (floor((coords[1] * scale) / 2) * 2) / scale;
	
	return output;
}

float4 MainPS (PixelInput input) : SV_Target
{
	float4 diffuse = tex2D(TextureSampler, input.TexCoord.xy);

	float4 newCol = float4(1, 1, 1, 0.85);
	
	float mult = 1.0 - sqrt(pow(input.Position[0] - Resolution[0] / 2.0, 2.0) + pow(input.Position[1] - Resolution[1] / 2.0, 2.0)) / max(Resolution[0], Resolution[1]);
		
	float4 coordEdit = PPEdit(input.TexCoord);
	float add = Bubble(coordEdit, float2(0.5, 0.4), float2(0, 0), float2(0.6, 0.6), 0.25 - (coordEdit[1]) * 0.25, 0.1);
	float add2 = Bubble(coordEdit, float2(-0.8, 1), float2(90, 60), float2(0.3, 0.3), 0.25 - (coordEdit[1]) * 0.20, 0.05);
	float add3 = Bubble(coordEdit, float2(0.1, 1.6), float2(180, 120), float2(0.1, 0.15), 0.25 - (coordEdit[1]) * 0.15, 0.025);
	
	for (int i = 0; i < 3; i++)
	{
		newCol[i] = input.Color[i] * Color[i];
		
		if (diffuse[0] > (140.0 / 255.0))
		{
			newCol[i] += (add + add2 + add3) / 5.0;
		}
		
		newCol[i] *= mult;
		newCol[i] += ColorAddition[i] * (1.0 - mult);
	}
	
	if (diffuse[0] > (140.0 / 255.0))
	{
		newCol[3] -= (add + add2 + add3) / 3.0;
	}

	return diffuse * newCol;
}

technique SpriteBatch
{
	pass
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};