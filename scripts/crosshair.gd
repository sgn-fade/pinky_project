extends CharacterBody2D

func _ready():
	EventBus.connect("crosshair_switch", Callable(self, "_on_crosshair_switch"))

func _process(delta):
	global_position = get_global_mouse_position()

func _on_crosshair_switch(type):
	if type == "ui":
		$part.emitting = true
	else:
		$part.emitting = false
		
	$Sprite2D.play(type)