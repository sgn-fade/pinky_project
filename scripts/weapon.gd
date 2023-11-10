extends Node
var equiped
var icon
var rarity
var modules = []
var Buttons_binds = {
	0 : "mouse_left_button",
	1 : "mouse_right_button",
	2 : "Q",
	3 : "R",
	4 : "X",
	5 : "C"
}
var Spells_buttons = {
	Buttons_binds[0] : null,
	Buttons_binds[1] : null,
	Buttons_binds[2]: null,
	Buttons_binds[3] : null,
	Buttons_binds[4] : null,
	Buttons_binds[5] : null,
}
var buttons = Spells_buttons.keys()

var module_position_list = [
	Vector2(418, 123),
	Vector2(649, 209),
	Vector2(733, 446),
	Vector2(649, 685),
	Vector2(418, 767),
	Vector2(187, 684),
	Vector2(103, 446),
	Vector2(188, 208)
]
var positions = module_position_list

var cells = []

func get_cells():
	return cells


func _ready():
	pass


func _init():
	EventBus.connect("add_module_to_place", self, "_on_add_module_to_place")
	randomize()
	for i in 4:
		cells.append(cell.new(positions.pop_at(randi()%positions.size())))


func input(event):
	var input_key = null
	if Input.is_action_just_pressed(String(Buttons_binds[0])):
		input_key = Buttons_binds[0]
	if Input.is_action_just_pressed(Buttons_binds[1]):
		input_key = Buttons_binds[1]
	if Input.is_action_just_pressed(Buttons_binds[2]):
		input_key = Buttons_binds[2]
	if Input.is_action_just_pressed(Buttons_binds[3]):
		input_key = Buttons_binds[3]
	if input_key != null and Spells_buttons[input_key] != null and Spells_buttons[input_key].get_ready():
		Spells_buttons[input_key].cast()
		EventBus.emit_signal("start_spell_cooldown", Spells_buttons[input_key].cooldown, input_key)


func _on_add_module_to_place(module, new, place, cell_index):
	if place == "equipment":
		cells[cell_index].module = module
		cells[cell_index].button = buttons.pop_at(0)
		Spells_buttons[cells[cell_index].button] = module
	if place == "inventory" and not new:
		buttons.append(cells[cell_index].button)
		cells[cell_index].button = null
		cells[cell_index].module = null
		

func remove_spell_from_button(button):
	var module = Spells_buttons[button]
	Spells_buttons[button] = null
	return module


func get_spell_from_button(button):
	return Spells_buttons[button]


class cell:
	var button
	var position
	var module
	var link
	func _init(pos):
		module = null
		position = pos
		
