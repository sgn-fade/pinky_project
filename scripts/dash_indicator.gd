extends TextureProgressBar

var cooldown = 4
var is_ready = true


func _ready():
	set_process(false)
	EventBus.connect("dash_cooldown", Callable(self, "_on_main_character_dash"))


func _process(delta):
	if value < max_value:
		value += delta * 1000
		return
		
	is_ready = true
	set_process(false)


func _on_main_character_dash():
	value = 0
	is_ready = false
	set_process(true)


