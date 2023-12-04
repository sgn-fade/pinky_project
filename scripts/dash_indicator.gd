extends TextureProgress

var cooldown = 4
var ready = true


func _ready():
	set_process(false)
	EventBus.connect("dash_cooldown", self, "_on_main_character_dash")


func _process(delta):
	if value < max_value:
		value += delta * 1000
		return
		
	ready = true
	set_process(false)


func _on_main_character_dash():
	value = 0
	ready = false
	set_process(true)


