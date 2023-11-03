extends StaticBody2D
onready var sprite = $Aspell
var timer := Timer.new()



var Spells_buttons = {
	"mouse_left_button" : null,
	"mouse_right_button" : null,
	"Q" : null,
	"R" : null,
}

func _ready():
	EventBus.connect("hands_play_animation", self, "play_animation")
	EventBus.connect("set_spell_to_button", self, "set_spell_to_button")
	timer.one_shot = false
	add_child(timer)
	cast_animation()
	


func _input(event):
	var input_key = null
	if Input.is_action_just_pressed("mouse_left_button"):
		input_key = "mouse_left_button"
	if Input.is_action_just_pressed("mouse_right_button"):
		input_key = "mouse_right_button"
	if Input.is_action_just_pressed("Q"):
		input_key = "Q"
	if Input.is_action_just_pressed("R"):
		input_key = "R"
	
	
	if input_key != null and Spells_buttons[input_key] != null and Spells_buttons[input_key].get_ready():
		Spells_buttons[input_key].cast()
		EventBus.emit_signal("start_spell_cooldown", Spells_buttons[input_key].cooldown, input_key)


func cast_animation():
	sprite.play("preparation")
	for i in 18:
		self.timer.start(0.06)
		yield(self.timer, "timeout")
		$light.energy += 0.07


func play_animation(time, animation_name):
	sprite.play(animation_name)
	timer.start(time)
	yield(timer, "timeout")
	cast_animation()
	EventBus.emit_signal("spell_animation_ended")

func set_spell_to_button(module, button):
	Spells_buttons[button] = module

func remove_spell_from_button(button):
	var module = Spells_buttons[button]
	Spells_buttons[button] = null
	return module

func get_spell_from_button(button):
	return Spells_buttons[button]
