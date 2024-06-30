shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
uniform float size: hint_range(-1.,1.) = 0.;
uniform vec2 source_size = vec2(16., 16.);

void fragment() {
	vec2 point = (1./TEXTURE_PIXEL_SIZE) / source_size;
	vec2 uv_temp = UV;
	uv_temp *= point;
	vec2 i_uv = floor(uv_temp);
	vec2 f_uv = fract(uv_temp); 
	float wave = max(0.,i_uv.x/i_uv.x*2.-1. - size);//replace sin(TIME) with "size" to custom param
	vec2 center = (f_uv*2.-1.);
	float shape = length(tan(center)*sin(center)-cos(center));
	shape = 1. - step(wave,shape);
	
	COLOR *= shape;
}