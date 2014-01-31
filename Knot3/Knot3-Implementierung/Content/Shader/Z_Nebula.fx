float4x4 World;
float4x4 View;
float4x4 Projection;

struct VertexToPixel
{
	float4 Position : POSITION0;
    float4 Color        : COLOR0;
};


struct PixelToFrame
{
    float4 Color        : COLOR0;
};

 
 VertexToPixel SimplestVertexShader( float4 inPos : POSITION0 )
 {
     VertexToPixel Output = (VertexToPixel) 0;
     
    float4 worldPosition = mul(inPos, World);
    float4 viewPosition = mul(worldPosition, View);
    Output.Color.rgba = mul(viewPosition, Projection);
	Output.Position = mul(viewPosition, Projection);

 
     return Output;
 }
 
 
 float4 OurFirstPixelShader(VertexToPixel PSIn) : COLOR0
 {
     //PixelToFrame Output = (PixelToFrame) 0;

	 //Output.Color = ;

	 float4 pos = PSIn.Color.rgba;
	 //float depth = P
 
     return float4(pos.z, pos.z, pos.z, 1);
 }
 
 technique Simplest
 {
     pass Pass0
     {
         VertexShader = compile vs_2_0 SimplestVertexShader();
         PixelShader = compile ps_2_0 OurFirstPixelShader();
     }
 }