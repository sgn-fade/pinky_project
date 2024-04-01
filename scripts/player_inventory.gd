extends Node
var item_count = 0
var module_array = []
var hands
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")
var weapon_card_scene = load("res://scenes/ui/weapon_card.tscn")
var choosed_slot = null
var empty_cell = load("res://scenes/ui/inventory_module_cell.tscn")
var inventory_object = load("res://scenes/ui/inventory/inventory_slot_object.tscn")
var inventory_cell = load("res://scenes/ui/inventory/inventory_cell.tscn")
var cells

func _ready():
	create_inventory_cells()
	EventBus.connect("add_item", Callable(self, "add_item"))



func create_inventory_cells():
	for y in range(199.5, 992, 132):
		for x in range(1042.5, 1900, 132):
			var cell = inventory_cell.instantiate()
			$item_grid/cells.add_child(cell)
			cell.global_position = Vector2(x, y)
				




func fill_cells():
	cells = Player.get_weapon().get_cells()
	for i in range(cells.size()):
		var current_cell = empty_cell.instantiate()
		$weapon/cells.add_child(current_cell)
		current_cell.position = cells[i].position
		current_cell.cell_index = i
		if cells[i].module != null:
			var slot = spell_slot_button_scene.instantiate()
			slot.init(cells[i].module, i)
			slot.position = cells[i].position
			slot.set_equiped(true)
			$weapon/cells.add_child(slot)
			EventBus.emit_signal("set_spell_icon_to_game", cells[i].module, cells[i].button)

func remove_all_cells():
	$weapon/weapon_slot.visible = true
	for child in $weapon/cells.get_children():
		child.queue_free()




func add_item(item):
	for cell in $item_grid/cells.get_children():
		if cell.is_empty():
			var object = inventory_object.instantiate()
			object.set_data(item.inventory_item)
			$item_grid/items.add_child(object)
			object.set_cell(cell)
			cell.set_object(object)
			return


