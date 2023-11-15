extends TextureProgress

var cooldown = 4
var ready = true
var msec_before_cooldown = 0

func _ready():
	set_process(false)
	EventBus.connect("dash_cooldown", self, "_on_main_character_dash")


func _process(delta):
	value = OS.get_ticks_msec() - msec_before_cooldown
	if value >= max_value:
		ready = true
		set_process(false)


func _on_main_character_dash():
	value = 0
	cooldown = 4
	ready = false
	max_value = 1000 * cooldown
	msec_before_cooldown = OS.get_ticks_msec()
	set_process(true)


