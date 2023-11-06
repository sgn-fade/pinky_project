extends Node
var selected_module = null
var item_count = 0
var module_array = []
var hands
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")
var choosed_cell = null
var module_position_list = {
	North = Vector2(418, 123),
	North_East = Vector2(649, 209),
	East = Vector2(733, 446),
	South_East = Vector2(649, 685),
	South = Vector2(418, 767),
	South_West = Vector2(187, 684),
	West = Vector2(103, 446),
	North_West = Vector2(188, 208)
}
var positions = module_position_list.values()

func _ready():
	
	EventBus.connect("add_module_to_place", self, "add_module_to_place")
	EventBus.connect("inventory_cell_choosed", self, "_on_inventory_cell_choosed")
	EventBus.connect("spell_slot_button_choosed", self, "_on_spell_slot_button_choosed")


func add_module_to_place(module, new, place):
	var slot = spell_slot_button_scene.instance()
	slot.init(module)
	if place == "inventory":
		$inventory/modules.add_child(slot)
		if new:
			slot.set_new_label(true)
	elif place == "equipment":
		slot.rect_position = choosed_cell.rect_position
		slot.set_equiped(true)
		add_child(slot)
		#slot.rect_position = positions.pop_at(randi() % positions.size())


func _on_inventory_cell_choosed(cell):
	choosed_cell = cell

func _on_spell_slot_button_choosed(slot, equiped):
	if !equiped:
		while(choosed_cell == null):
			yield(get_tree().create_timer(0.0001), "timeout")
			if Input.is_action_just_pressed("mouse_right_button"):
				return 
		add_module_to_place(slot.module, false, "equipment")
		slot.queue_free()
		


func remove_spell_from_slot(slot, button):
	hands = get_node("/root/World/player/hands/h_spell")
	if hands.get_spell_from_button(button) != null and Input.is_action_just_pressed("mouse_right_button"):
		EventBus.emit_signal("add_module_to_place", hands.remove_spell_from_button(button), false)
		EventBus.emit_signal("remove_spell_from_inventory", button)	


func set_button_texture(slot, button):
	hands = get_node("/root/World/player/hands/h_spell")
	if selected_module != null:
		if hands.get_spell_from_button(button) != null:
			EventBus.emit_signal("add_module_to_place", hands.remove_spell_from_button(button), false)
		EventBus.emit_signal("set_spell_to_button", selected_module.module, button)
		EventBus.emit_signal("set_spell_icon_to_game_ui", selected_module.module.spell_icon, button)
		selected_module = null
