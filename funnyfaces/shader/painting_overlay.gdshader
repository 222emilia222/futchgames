shader_type canvas_item;

uniform sampler2D paint_texture;

void fragment()
{
	vec4 col = texture(TEXTURE, UV);
	vec4 paint = texture(paint_texture, UV);

	//vec4 aCol = mix(col, paint, paint.a);
	//vec4 bCol = col + paint - vec4(2.0) * col * paint;
	//vec4(1.0) - (vec4(1.0) - col) * (vec4(1.0) - paint);
	//COLOR = mix(bCol, aCol, paint.a);

	vec4 colorMix = mix(col + paint, paint, paint.a);
	COLOR.rgb = colorMix.rgb;
	COLOR.a = col.a;
}
