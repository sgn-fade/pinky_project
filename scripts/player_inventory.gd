extends Node
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")
@onready var fireball_spell = load("res://scripts/spells/fireball_spell.gd")
@onready var fire_pillar_spell = load("res://scripts/spells/fire_pillar_spell.gd")
@onready var fire_teleport_spell = load("res://scripts/spells/fire_teleport_spell.gd")
@onready var fire_eye_spell = load("res://scripts/spells/fire_eye_spell.gd")
@onready var fire_spear_spell = load("res://scripts/spells/fire_spear_spell.gd")
@onready var smite = load("res://scripts/spells/melee_spells/smite.gd")
var weapon_card_scene = load("res://scenes/ui/weapon_card.tscn")
var empty_cell = load("res://scenes/ui/inventory/module_cell.tscn")
var inventory_object = load("res://scenes/ui/inventory/inventory_slot_object.tscn")
var inventory_cell = load("res://scenes/ui/inventory/inventory_cell.tscn")
var cells


func _ready():
	create_inventory_cells()
	EventBus.connect("add_item", Callable(self, "add_item"))
	var wand = load("res://scripts/weapons/magic_weapons/old_goblins_magic_wand.gd")
	var potion = load("res://scripts/drops/potion.gd")
	EventBus.emit_signal("add_item", fire_pillar_spell.new())
	EventBus.emit_signal("add_item", fireball_spell.new())
	EventBus.emit_signal("add_item", fire_spear_spell.new())
	EventBus.emit_signal("add_item", wand.new())
	EventBus.emit_signal("add_item", potion.new())
	EventBus.emit_signal("add_item", potion.new())
	EventBus.emit_signal("add_item", potion.new())


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
			var item = inventory_object.instantiate()
			item.set_data(cells[i].module.inventory_item)
			item.position = current_cell.position
			$item_grid/items.add_child(item)
			item.set_cell(current_cell)
			item.visible = false
			current_cell.restore_object(item)
			EventBus.emit_signal("set_spell_icon_to_game", cells[i].module, cells[i].button)


func remove_all_cells():
	EventBus.emit_signal("clear_spell_icons")
	for child in $weapon/cells.get_children():
		child.queue_free()
	for child in $item_grid/items.get_children():
		if child.current_cell.slot_type == "spell":
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


func _on_weapon_cell_button_pressed():
	$default_right_side.visible = false
	hide_objects("weapon")
	$weapon.visible = true


func _on_weapon_cell_set_weapon_to_ui(weapon):
	$weapon/weapon_rarity_bg.show_weapon(Player.get_weapon())
	if weapon == null:
		remove_all_cells()
		return
	fill_cells()


func _on_back_arrow_pressed():
	$weapon.visible = false
	hide_objects("spell")
	$default_right_side.visible = true


func hide_objects(type):
	for child in $item_grid/items.get_children():
		if child.current_cell.slot_type == type:
			child.visible = false
		else:
			child.visible = true
