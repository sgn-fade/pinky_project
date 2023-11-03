extends Sprite

var light_offset = 0.001
onready var light = $Light2D

func _process(delta):
	if light.texture_scale >= 1 or light.texture_scale <= 0.9:
		light_offset *= -1
	light.texture_scale += light_offset
		
