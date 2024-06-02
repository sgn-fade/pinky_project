extends Area2D
@onready var sprite = $body/sprite
@onready var body = $body
var old_goblins_magic_wand = load("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd")
var fire_book_tome_1 = load("res://scripts/weapons/magic_weapons/fire_book_tome_1.gd")
var goblin_sword = load("res://scripts/weapons/melee/sword.gd")
var weapon_list = []
var weapon = null


func _ready():
	weapon_list.append(old_goblins_magic_wand)
	weapon_list.append(fire_book_tome_1)
	weapon_list.append(goblin_sword)
	weapon = weapon_list.pop_at(randi()% weapon_list.size()).new()
	#weapon = goblin_sword.new()
	EventBus.connect("go_to_hub", Callable(self, "go_to_hub"))


func _process(delta):
	body.z_index = global_position.y / 2
	body.position.y = sin(Time.get_ticks_msec()  * 0.003) * 2
	if overlaps_body(Player.get_body()):
		sprite.frame = 1
		EventBus.emit_signal("show_weapon_stats_on_game_screen", weapon)
		
		if Input.is_action_just_pressed("E"):
			set_process(false)
			body.queue_free()
			$end_particles.emitting = true
			EventBus.emit_signal("add_item", weapon)
			EventBus.emit_signal("hide_weapon_stats_on_game_screen")
			await get_tree().create_timer(0.3).timeout
			queue_free()
	elif sprite.frame == 1:
		EventBus.emit_signal("hide_weapon_stats_on_game_screen")
		sprite.frame = 0

