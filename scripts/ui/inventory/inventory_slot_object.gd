extends CharacterBody2D
var data = null
var data_type = null
var cell_pos = null
var mouse_in_area = false

func set_data(new_data, new_data_type, icon, background = null):
	data = new_data
	data_type = new_data_type
	$icon.texture = icon
	$background.texture = background


func get_data():
	return data

func _input(event):
	if Input.is_action_pressed("mouse_left_button") and mouse_in_area:
		set_process(true)
	if Input.is_action_just_released("mouse_left_button") and mouse_in_area:
		set_to_cell_position()
		set_process(false)
	

func _ready():
	set_process(false)

func _process(delta):
	global_position = get_global_mouse_position()

func set_to_cell_position():
	global_position = cell_pos

func set_cell_pos(pos):
	cell_pos = pos

func _on_mouse_entered():
	mouse_in_area = true


func _on_mouse_exited():
	mouse_in_area = false



