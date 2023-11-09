extends Sprite

var button_text : String

func _ready():
	button_text = Options.Buttons_binds.get(get_parent().name)
	if button_text.length() == 1:
		get_node("Button_text").set_text(button_text)
	else: 
		match (button_text):
			"mouse_left_button":
				texture = load("res://sprites/ui/mouse_button_ui.png")
			"mouse_right_button":
				texture = load("res://sprites/ui/mouse_button_ui.png")
				flip_h = true
