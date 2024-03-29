extends CharacterBody2D
var data = null
var data_type = null
var current_cell = null
var target_cell = null
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
		z_index = 10
		set_process(true)
	if Input.is_action_just_released("mouse_left_button") and mouse_in_area:
		z_index = 0
		set_to_cell()
		set_process(false)
	

func _ready():
	set_process(false)

func _process(delta):
	global_position = get_global_mouse_position()

func set_to_cell():
	if target_cell == null:
		global_position = current_cell.get_pos()
		return
	target_cell.swap_objects(current_cell, self)
	set_cell(target_cell)
	target_cell = null

func set_cell(cell):
	current_cell = cell
	global_position = current_cell.get_pos()

func get_cell():
	return current_cell

func set_target_cell(cell):
	target_cell = cell



func _on_mouse_entered():
	mouse_in_area = true
func _on_mouse_exited():
	mouse_in_area = false



