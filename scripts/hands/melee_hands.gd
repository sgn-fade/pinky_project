extends "res://scripts/hands/clear_hands.gd"

var combo_timer = Timer.new()
var combo_names = ["hit_1", "hit_2", "hit_3"]
func _ready():
	EventBus.connect("hands_play_animation", Callable(self, "play_animation"))
	add_child(combo_timer)
	combo_timer.one_shot = true


func _process(delta):
	if $anim.current_animation == "":
		look_at(get_global_mouse_position())


func play_animation(animation_time, animation_name):
	super.play_animation(0, animation_name)
	get_node("anim").play(animation_name)


func _input(event):
	if Input.is_action_just_pressed("mouse_left_button") and Player.get_state() != Player.get_body().States.SPELL:
		if combo_timer.time_left <= 0:
			combo_names = ["hit_1", "hit_2", "hit_3"]
		var hit_name = combo_names.pop_front()
		combo_names.push_back(hit_name)
		hit(hit_name)


func hit(hit_count):
	Player.get_body().set_speed(20)
	play_animation(0, hit_count)
	await get_node("anim").animation_finished
	combo_timer.start(1)
	Player.get_body().set_speed(80)
	play_animation(0, "idle")

