shader_type spatial;
render_mode unshaded, fog_disabled;

uniform sampler2D screen_texture : hint_screen_texture, repeat_disable, filter_nearest;
uniform sampler2D game_texture : source_color, repeat_disable, filter_nearest;
uniform sampler2D sight_texture : source_color, repeat_disable, filter_nearest;
uniform sampler2D world_texture : source_color, repeat_disable, filter_nearest;
uniform float outside_staturation_offset = -0.1;
uniform float outside_value_offset = -0.1;

void vertex() {
	  POSITION = vec4(VERTEX.xy, 1.0, 1.0);
}

void fragment() {
	
	float sight_color_r = texture(sight_texture, SCREEN_UV).r;
	vec3 output_color = texture(game_texture, SCREEN_UV).rgb;
	
	if (sight_color_r < 1.00) {
		
		output_color = texture(world_texture, SCREEN_UV).rgb;
		
	}
	
	ALBEDO.rgb = output_color.rgb;
	
}
