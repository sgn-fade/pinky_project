extends Node
onready var module_list = $module_list
onready var slot1 = $"%1"
onready var slot2 = $"%2"
onready var slot3 = $"%3"
onready var slot4 = $"%4"
var selected_module = null
var item_count = 0
var module_array = []
var hands
var spell_slot_button_scene = load("res://scenes/ui/spell_slot_button.tscn")


func _ready():
	EventBus.connect("add_module_to_inventory", self, "add_module_to_inventory")
	EventBus.connect("spell_slot_button_pressed", self, "_on_spell_slot_button_pressed")


func add_module_to_inventory(module, new):
	var slot = spell_slot_button_scene.instance()
	$inventory/modules.add_child(slot)
	if not new:
		slot.new.visible = false
	slot.set_button_texture(module)
	
	



func _on_spell_slot_button_pressed(button):
	selected_module = button


func _on_1_pressed():
	set_button_texture(slot1, "mouse_left_button")
	remove_spell_from_slot(slot1, "mouse_left_button")
func _on_2_pressed():
	set_button_texture(slot2, "mouse_right_button")
	remove_spell_from_slot(slot2, "mouse_right_button")
	
func _on_3_pressed():
	set_button_texture(slot3, "Q")
	remove_spell_from_slot(slot3, "Q")
	
func _on_4_pressed():
	set_button_texture(slot4, "R")
	remove_spell_from_slot(slot4, "R")
	


func remove_spell_from_slot(slot, button):
	hands = get_node("/root/World/player/hands/h_spell")
	if hands.get_spell_from_button(button) != null and Input.is_action_just_pressed("mouse_right_button"):
		set_default_button_texture(slot)
		EventBus.emit_signal("add_module_to_inventory", hands.remove_spell_from_button(button), false)
		EventBus.emit_signal("remove_spell_icon_from_game_ui", button)


func set_default_button_texture(slot):
	slot.get_node("spell_icon").texture = null
	slot.texture_pressed = load("res://sprites/ui/empty_inventory_slot_pressed.png")
	slot.texture_normal = load("res://sprites/ui/empty_inventory_slot.png")
	slot.texture_hover = load("res://sprites/ui/empty_inventory_slot_focused.png")


func set_button_texture(slot, button):
	hands = get_node("/root/World/player/hands/h_spell")
	if selected_module != null:
		if hands.get_spell_from_button(button) != null:
			EventBus.emit_signal("add_module_to_inventory", hands.remove_spell_from_button(button), false)
		EventBus.emit_signal("set_spell_to_button", selected_module.module, button)
		EventBus.emit_signal("set_spell_icon_to_game_ui", selected_module.module.spell_icon, button)
		slot.get_node("spell_icon").texture = selected_module.module.spell_icon
		slot.texture_pressed = load("res://sprites/ui/%s_module_button_pressed.png" % selected_module.module.rarity)
		slot.texture_normal = load("res://sprites/ui/%s_module_button_state.png" % selected_module.module.rarity)
		slot.texture_hover = load("res://sprites/ui/%s_module_button_hover.png" % selected_module.module.rarity)
		selected_module.queue_free()
		selected_module = null
