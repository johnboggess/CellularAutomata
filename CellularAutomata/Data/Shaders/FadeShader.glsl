-- Fade
#version 430

layout (binding=0, rgba8) uniform image2D Texture;

layout (local_size_x = $SizeX$, local_size_y = $SizeY$) in;
void main()
{
	vec4 c = imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy));
	c = c-0.1;
	c = max(c, vec4(0,0,0,0));

	imageStore(Texture, ivec2(gl_GlobalInvocationID.xy), c);
}