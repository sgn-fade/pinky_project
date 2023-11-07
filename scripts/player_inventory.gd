extends Node
var selected_module = null
var item_count = 0
var module_array = []
var hands
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")
var choosed_slot = null
var empty_cell = load("res://scenes/ui/inventory_module_cell.tscn")
var cells

func _ready():
	fill_cells()
	EventBus.connect("add_module_to_place", self, "add_module_to_place")
	EventBus.connect("inventory_cell_choosed", self, "_on_inventory_cell_choosed")
	EventBus.connect("spell_slot_button_choosed", self, "_on_spell_slot_button_choosed")


func fill_cells():
	cells = Player.get_weapon().get_cells()
	for i in range(cells.size()):
		var current_cell = empty_cell.instance()
		add_child(current_cell)
		current_cell.rect_position = cells[i].position
		current_cell.cell_index = i


func add_module_to_place(module, is_new, place, cell_index):
	var slot = spell_slot_button_scene.instance()
	slot.init(module)
	if place == "inventory":
		$inventory/modules.add_child(slot)
		if is_new:
			slot.set_new_label(true)
	elif place == "equipment":
		slot.rect_position = cells[cell_index].position
		slot.set_equiped(true)
		add_child(slot)


func _on_inventory_cell_choosed(cell):
	if choosed_slot != null:
		EventBus.emit_signal("add_module_to_place", choosed_slot.module, false, "equipment", cell.cell_index)
		choosed_slot.queue_free()
func _on_spell_slot_button_choosed(slot, equiped):
	if !equiped:
		choosed_slot = slot
	else:
		EventBus.emit_signal("add_module_to_place", slot.module, false, "inventory", -1)
		slot.queue_free()


