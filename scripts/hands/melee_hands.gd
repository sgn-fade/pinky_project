extends "res://scripts/hands/clear_hands.gd"
onready var sword = load("res://scenes/weapons/melee/sword/sword.tscn")
onready var hit_scene = load("res://scenes/hit_particle.tscn")
var sword_obj

var combo_timer = Timer.new()

func _ready():
	add_child(combo_timer)
	combo_timer.one_shot = true
	sword_obj = sword.instance()
	add_child(sword_obj)
	sword_obj.position = $sword_pos.position

func play_animation(animation_time, animation_name):
	.play_animation(0, animation_name)
	sword_obj.get_node("anim").play(animation_name)

func _input(event):
	if Input.is_action_just_pressed("mouse_left_button") and Player.get_state() != Player.get_body().States.SPELL:
		if combo_timer.time_left > 0:
			second_combo()
			return
		first_combo()
		
func first_combo():

	Player.set_state(Player.get_body().States.SPELL)
	play_animation(0, "base_sword_combo_first")
	Player.get_body().add_child(hit_scene.instance())
	Player.get_body().push_body()
	yield($sprite, "animation_finished")
	combo_timer.start(1)
	
	Player.set_state(Player.get_body().States.IDLE)
	play_animation(0, "idle")
	

func second_combo():
	Player.set_state(Player.get_body().States.SPELL)
	play_animation(0, "base_sword_combo_third")
	Player.get_body().add_child(hit_scene.instance())
	Player.get_body().push_body()
	yield($sprite, "animation_finished")
	Player.set_state(Player.get_body().States.IDLE)
	play_animation(0, "idle")
	
	
	
	
	
	
