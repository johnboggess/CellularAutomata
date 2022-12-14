-- CellularAutomata
#version 430

layout (binding=0, rgba8) uniform image2D FrontBuffer;
layout (binding=1, rgba8) uniform image2D BackBuffer;
uniform vec2 ClickLocation;

layout (local_size_x = 10, local_size_y = 10) in;

float growth(float sum)
{
	if(sum < 2.0 || sum > 3.0)
		return -1.;
	if(sum > 2.0 && sum <= 3.0)
		return 1.0;
	return 0.0;
}

int mod(int top, int bottom)
{
	return int(top-(bottom * floor(top/bottom)));
}

float convolution(float[9] kernel)
{
	int size = kernel.length();
	int side = int(floor(sqrt(float(size))));
	float result = 0.;

	for(int i = 0; i < size; i++)
	{
		int x = i % side;
		int y = i / side;
		
		x = x - (side / 2 );
		y = y - (side / 2 );
		
		vec4 value = imageLoad(BackBuffer, ivec2(gl_GlobalInvocationID.xy)+ivec2(x,y));
		value *= kernel[i];
		result += value.a;
	}
	return result;
}

void main()
{
	vec2 id = vec2(float(gl_GlobalInvocationID.x), float(gl_GlobalInvocationID.y));
	vec2 click = vec2(float(ClickLocation.x), float(ClickLocation.y));

	float len = length(id-click);

	vec4 back = imageLoad(BackBuffer, ivec2(gl_GlobalInvocationID.xy));
	
	float[9] kernel = float[9] (1,1,1,1,0,1,1,1,1);
	float convo = convolution(kernel);
	float grow = growth(convo);

	grow = clamp(grow+back.a,0,1);
	imageStore(FrontBuffer, ivec2(gl_GlobalInvocationID.xy), vec4(grow));
	if(len < 100.0)
		imageStore(FrontBuffer, ivec2(gl_GlobalInvocationID.xy), vec4(1));
		
}