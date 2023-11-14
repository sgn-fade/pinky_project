extends Area2D
onready var sprite = $body/sprite
onready var body = $body
var module = null
var module_list = []
onready var fireball_spell = load("res://scripts/spells/fireball_spell.gd")
onready var fire_pillar_spell = load("res://scripts/spells/fire_pillar_spell.gd")
onready var fire_teleport_spell = load("res://scripts/spells/fire_teleport_spell.gd")
onready var fire_eye_spell = load("res://scripts/spells/fire_eye_spell.gd")
onready var fire_spear_spell = load("res://scripts/spells/fire_spear_spell.gd")

func _ready():
	module_list.append(fire_eye_spell)
	module_list.append(fire_pillar_spell)
	module_list.append(fire_teleport_spell)
	module_list.append(fire_spear_spell)
	module_list.append(fireball_spell)
	module = module_list[randi() % 5].new()

func _process(delta):
	body.z_index = global_position.y / 2
	body.position.y = sin(OS.get_ticks_msec()  * 0.003) * 2
	
	if overlaps_body(Player.get_body()):
		sprite.frame = 1
		EventBus.emit_signal("show_module_stats_on_game_screen", module)
		
		if Input.is_action_just_pressed("pick_up"):
			set_process(false)
			body.queue_free()
			$end_particles.emitting = true
			EventBus.emit_signal("add_module_to_place", module, true, "inventory", -1)
			EventBus.emit_signal("hide_module_stats_on_game_screen")
			yield(get_tree().create_timer(0.3), "timeout")
			queue_free()
	elif sprite.frame == 1:
		EventBus.emit_signal("hide_module_stats_on_game_screen")
		sprite.frame = 0
