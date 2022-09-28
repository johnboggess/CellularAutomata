-- ToBackBuffer
#version 430

layout (binding=0, rgba8) uniform image2D FrontBuffer;
layout (binding=1, rgba8) uniform image2D BackBuffer;
uniform vec2 ClickLocation;

layout (local_size_x = 10, local_size_y = 10) in;
void main()
{
	vec2 id = vec2(float(gl_GlobalInvocationID.x), float(gl_GlobalInvocationID.y));

	vec4 front = imageLoad(FrontBuffer, ivec2(gl_GlobalInvocationID.xy));
	front = clamp(front, 0.,.25);
	imageStore(BackBuffer, ivec2(gl_GlobalInvocationID.xy), front);
}