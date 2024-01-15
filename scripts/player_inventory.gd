extends Node
var item_count = 0
var module_array = []
var hands
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")
var weapon_card_scene = load("res://scenes/ui/weapon_card.tscn")
var choosed_slot = null
var empty_cell = load("res://scenes/ui/inventory_module_cell.tscn")
var cells

func _ready():
	EventBus.connect("add_module_to_place", Callable(self, "add_module_to_place"))
	EventBus.connect("remove_weapon_from_slot", Callable(self, "_on_remove_weapon_from_slot"))
	EventBus.connect("add_weapon_to_inventory", Callable(self, "add_weapon_to_inventory"))
	EventBus.connect("inventory_cell_choosed", Callable(self, "_on_inventory_cell_choosed"))
	EventBus.connect("weapon_in_inventory_choosed", Callable(self, "_on_weapon_in_inventory_choosed"))
	EventBus.connect("spell_slot_button_choosed", Callable(self, "_on_spell_slot_button_choosed"))
	EventBus.connect("spell_slot_button_unselected", Callable(self, "_on_spell_slot_button_unselected"))


func _process(detla):
	$coins_indicator/count.text = str(Player.get_money())

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
			EventBus.emit_signal("set_spell_icon_to_game", cells[i].module.spell_icon, cells[i].button)

func remove_all_cells():
	$weapon/weapon_slot.visible = true
	for child in $weapon/cells.get_children():
		child.queue_free()

func add_weapon_to_inventory(weapon):
	var card = weapon_card_scene.instantiate()
	$weapon_inventory/weapons.add_child(card)
	card.init(weapon)
	card.get_node("main_button/weapon_texture").texture = weapon.icon


func add_module_to_place(module, is_new, place, cell_index):
	var slot = spell_slot_button_scene.instantiate()
	slot.init(module, cell_index)
	if place == "inventory":
		$inventory/modules.add_child(slot)
		if is_new:
			slot.set_new_label(true)
	elif place == "equipment":
		Player.get_weapon().add_module_to_weapon(module, is_new, place, cell_index)
		slot.position = cells[cell_index].position
		slot.set_equiped(true)
		$weapon/cells.add_child(slot)


func _on_inventory_cell_choosed(cell):
	if choosed_slot != null:
		EventBus.emit_signal("add_module_to_place", choosed_slot.module, false, "equipment", cell.cell_index)
		choosed_slot.queue_free()


func _on_spell_slot_button_choosed(slot, equiped):
	if !equiped:
		choosed_slot = slot
	else:
		EventBus.emit_signal("add_module_to_place", slot.module, false, "inventory", -1)
		Player.get_weapon().remove_module_from_weapon(slot.module, slot.index)
		slot.queue_free()

func _on_spell_slot_button_unselected():
	await get_tree().create_timer(1).timeout
	choosed_slot = null
	EventBus.emit_signal("spell_cells_light_off")


func _on_weapons_pressed():
	$inventory.visible = false
	$weapon_inventory.visible = true
	$module_layout.visible = false
	$weapon.visible = true
func _on_modules_pressed():
	$inventory.visible = true
	$weapon_inventory.visible = false
	$module_layout.visible = false
	$weapon.visible = true
	
func _on_layout_pressed():
	$inventory.visible = false
	$weapon_inventory.visible = false
	$module_layout.visible = true
	$weapon.visible = false


func _on_remove_weapon_from_slot():
	EventBus.emit_signal("add_weapon_to_inventory", Player.get_weapon())
	EventBus.emit_signal("clear_spell_icons")
	EventBus.emit_signal("switch_hands_stance", null)
	Player.set_weapon(null)
	remove_all_cells()


func _on_weapon_in_inventory_choosed(weapon):
	if Player.get_weapon() != null:
		EventBus.emit_signal("add_weapon_to_inventory", Player.get_weapon())
		EventBus.emit_signal("clear_spell_icons")
		remove_all_cells()
	Player.set_weapon(weapon)
	EventBus.emit_signal("switch_hands_stance", weapon)
	fill_cells()
	$weapon/weapon_slot.visible = false
	$weapon/weapon_rarity_bg.show_weapon(weapon)



func _on_slot_1_weapon_pressed():
	swap_weapon_slot(1)


func _on_slot_2_weapon_pressed():
	swap_weapon_slot(2)


func swap_weapon_slot(slot):
	Player.set_weapon_current_slot(slot)
	EventBus.emit_signal("clear_spell_icons")
	EventBus.emit_signal("switch_hands_stance", Player.get_weapon())
	remove_all_cells()
	$weapon/weapon_rarity_bg.visible = false
	$weapon/weapon_slot.visible = true
	
	if Player.get_weapon() != null:
		fill_cells()
		$weapon/weapon_slot.visible = false
		$weapon/weapon_rarity_bg.show_weapon(Player.get_weapon())
