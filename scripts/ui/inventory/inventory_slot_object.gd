extends Control
var data = null
var data_type = null

var mouse_in_area = false

func set_data(new_data, new_data_type, icon, background = null):
	data = new_data
	data_type = new_data_type
	$icon.texture = icon
	$background.texture = background


func get_data():
	return data

func _input(event):
	if Input.is_action_just_pressed("mouse_left_button") and mouse_in_area:
		print(true)
		set_process(true)
		
	if Input.is_action_just_released("mouse_left_button") and mouse_in_area:
		pass

func _ready():
	set_process(false)

func _process(delta):
	global_position = get_global_mouse_position()




func _on_mouse_entered():
	print(true)
	mouse_in_area = true


func _on_mouse_exited():
	print(false)
	mouse_in_area = false
