shader_type canvas_item;

uniform sampler2D paint_texture;

void fragment()
{
	vec4 col = texture(TEXTURE, UV);
	vec4 paint = texture(paint_texture, UV);

	if(paint.a == 1.0)
	{
		COLOR = mix(col, paint, paint.a);
		COLOR.a = col.a;
	}
	else
	{
	   //COLOR = vec4(1.0) - (vec4(1.0) - col)* (vec4(1.0) - paint);

	}
	//COLOR = paint.a < 0.5 ? (2.0 * col * paint) : (vec4(1.0) - 2.0 * (vec4(1.0) - col) * (vec4(1.0) - paint));

}
