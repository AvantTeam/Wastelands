#if OPENGL
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0_level_9_1
    #define PS_SHADERMODEL ps_4_0_level_9_1
#endif

extern float2 Resolution;
sampler TextureSampler : register(s0);

struct PixelInput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float4 TexCoord : TEXCOORD0;
};

float4 MainPS(PixelInput input) : SV_Target
{
	float4 diffuse = tex2D(TextureSampler, input.TexCoord.xy);

	float4 newCol = float4(1, 1, 1, input.Color[3]);
	
	float mult = 1.0 - sqrt(pow(input.Position[0] - Resolution[0] / 2.0, 2.0) + pow(input.Position[1] - Resolution[1] / 2.0, 2.0)) / max(Resolution[0], Resolution[1]);
	for (int i = 0; i < 3; i++)
	{
		newCol[i] = input.Color[i] * mult;
	}
	newCol[0] += 0.2 * (1.0 - mult);
	newCol[1] += 0.1 * (1.0 - mult);

	return diffuse * newCol;
}

technique SpriteBatch
{
	pass
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};