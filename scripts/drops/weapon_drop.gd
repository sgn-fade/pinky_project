extends Area2D
onready var sprite = $body/sprite
onready var body = $body
var weapon = null
var weapon_list = []
func _ready():
	EventBus.connect("go_to_hub", self, "go_to_hub")
	weapon = weapon_list[randi() % 5].new()

func go_to_hub():
	queue_free()


func _process(delta):
	body.z_index = global_position.y / 2
	body.position.y = sin(OS.get_ticks_msec()  * 0.003) * 2
	if overlaps_body(Player.get_body()):
		sprite.frame = 1
		#EventBus.emit_signal("show_module_stats_on_game_screen", module)
		
		if Input.is_action_just_pressed("pick_up"):
			set_process(false)
			body.queue_free()
			$end_particles.emitting = true
			#EventBus.emit_signal("add_module_to_inventory", module, true)
			#EventBus.emit_signal("hide_module_stats_on_game_screen")
			yield(get_tree().create_timer(0.3), "timeout")
			queue_free()
	elif sprite.frame == 1:
		#EventBus.emit_signal("hide_module_stats_on_game_screen")
		sprite.frame = 0
