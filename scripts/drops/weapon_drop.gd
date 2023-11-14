extends Area2D
onready var sprite = $body/sprite
onready var body = $body
var old_goblins_magic_wand = load("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd")
var fire_book_tome_1 = load("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd")
var weapon_list = []
var weapon = null
func _ready():
	weapon_list.append(old_goblins_magic_wand)
	weapon_list.append(fire_book_tome_1)
	weapon = weapon_list.pop_at(randi()%2).new()
	print(weapon)
	EventBus.connect("go_to_hub", self, "go_to_hub")

func go_to_hub():
	queue_free()


func _process(delta):
	body.z_index = global_position.y / 2
	body.position.y = sin(OS.get_ticks_msec()  * 0.003) * 2
	if overlaps_body(Player.get_body()):
		sprite.frame = 1
		EventBus.emit_signal("show_weapon_stats_on_game_screen", weapon)
		
		if Input.is_action_just_pressed("E"):
			set_process(false)
			body.queue_free()
			$end_particles.emitting = true
			EventBus.emit_signal("add_weapon_to_inventory", weapon)
			EventBus.emit_signal("hide_weapon_stats_on_game_screen")
			yield(get_tree().create_timer(0.3), "timeout")
			queue_free()
	elif sprite.frame == 1:
		EventBus.emit_signal("hide_weapon_stats_on_game_screen")
		sprite.frame = 0