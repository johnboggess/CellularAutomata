-- CellularAutomata
#version 430

layout (binding=0, rgba8) uniform image2D FrontBuffer;
layout (binding=1, rgba8) uniform image2D BackBuffer;
uniform vec2 ClickLocation;

layout (local_size_x = 10, local_size_y = 10) in;

float convolution(float[9] kernel, ivec2 center)
{
	int size = kernel.length();
	int side = int(floor(sqrt(float(size))));
	float result = 0.;

	for(int i = 0; i < size; i++)
	{
		int x = i % size;
		int y = i / side;
		
		x = x - (side / 2 );
		y = y - (side / 2 );
		
		x += center.x;
		y += center.y;
		
		vec4 value = imageLoad(BackBuffer, ivec2(gl_GlobalInvocationID.xy));
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
	float convo = convolution(kernel, ivec2(gl_GlobalInvocationID.xy));
	if(len < 100.0)
		imageStore(FrontBuffer, ivec2(gl_GlobalInvocationID.xy), vec4(.01)+convo);
}