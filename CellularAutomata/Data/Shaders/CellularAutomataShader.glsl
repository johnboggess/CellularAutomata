﻿-- CellularAutomata
#version 430

layout (binding=0, rgba8) uniform image2D FrontBuffer;
layout (binding=1, rgba8) uniform image2D BackBuffer;
uniform vec2 ClickLocation;

layout (local_size_x = 10, local_size_y = 10) in;
void main()
{
	vec2 id = vec2(float(gl_GlobalInvocationID.x), float(gl_GlobalInvocationID.y));
	vec2 click = vec2(float(ClickLocation.x), float(ClickLocation.y));

	float len = length(id-click);

	vec4 back = imageLoad(BackBuffer, ivec2(gl_GlobalInvocationID.xy));
	if(len < 100.0)
		imageStore(FrontBuffer, ivec2(gl_GlobalInvocationID.xy), vec4(.25,.25,.25,.25)+back);
}