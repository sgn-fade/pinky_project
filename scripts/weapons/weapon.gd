extends Node

var equiped
var icon
var rarity
var modules = []
var Buttons_binds = Options.Buttons_binds
var type = "none"
var damage

func get_type():
	return type

var Spells_buttons = {
	Buttons_binds["slot1"] : null,
	Buttons_binds["slot2"] : null,
	Buttons_binds["slot3"] : null,
	Buttons_binds["slot4"] : null,
	Buttons_binds["slot5"] : null,
	Buttons_binds["slot6"] : null,
}
var buttons = Buttons_binds.keys()

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
	if Input.is_action_just_pressed(Buttons_binds["slot1"]):
		input_key = "slot1"
	if Input.is_action_just_pressed(Buttons_binds["slot2"]):
		input_key = "slot2"
	if Input.is_action_just_pressed(Buttons_binds["slot3"]):
		input_key = "slot3"
	if Input.is_action_just_pressed(Buttons_binds["slot4"]):
		input_key = "slot4"
	if Input.is_action_just_pressed(Buttons_binds["slot5"]):
		input_key = "slot5"
	if Input.is_action_just_pressed(Buttons_binds["slot6"]):
		input_key = "slot6"
	if input_key != null and Spells_buttons[Buttons_binds[input_key]] != null and Spells_buttons[Buttons_binds[input_key]].get_ready():
	
		Spells_buttons[Buttons_binds[input_key]].cast()
		EventBus.emit_signal("start_spell_cooldown", Spells_buttons[Buttons_binds[input_key]].cooldown, input_key)


func _on_add_module_to_place(module, new, place, cell_index):
	if place == "equipment":
		cells[cell_index].module = module
		cells[cell_index].button = buttons.pop_at(0)
		Spells_buttons[Buttons_binds[cells[cell_index].button]] = module
		EventBus.emit_signal("set_spell_icon_to_game", module.spell_icon, cells[cell_index].button)
	if place == "inventory" and not new:
		buttons.append(cells[cell_index].button)
		cells[cell_index].button = null
		cells[cell_index].module = null

func remove_spell_from_button(button):
	EventBus.emit_signal("remove_spell_icon_from_game", button)
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
		

