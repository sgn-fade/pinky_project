extends Control

var drag_source = null
var drag_destination = null
var drag_icon = null

		
func _ready():
	EventBus.connect("set_spell_icon_to_game", Callable(self, "_on_set_spell_icon_to_game"))
	EventBus.connect("remove_spell_icon_from_game", Callable(self, "_on_remove_spell_icon_from_game_ui"))
	EventBus.connect("clear_spell_icons", Callable(self, "clear_spell_icons"))
	
func _on_remove_spell_icon_from_game(button):
	get_node("%s" % button).texture_normal = null


func _on_set_spell_icon_to_game(module, button):
	get_node("%s" % button).texture_normal = module.spell_icon

func clear_spell_icons():
	for node in get_children():
		node.texture_normal = null
	
func _input(event):
	if Input.is_action_just_released("mouse_left_button") and drag_source != null:
		if drag_destination == null:
			drag_source.texture_normal = drag_icon
		else:
			Player.get_weapon().swap_modules(drag_source.name , drag_destination.name)
			drag_destination.texture_normal = drag_icon
		drag_icon = null
		drag_source = null
		drag_destination = null


func _on_mouse_entered_to_destionation(object):
	if drag_source != null:
		drag_destination = object
		return


func _on_mouse_exit_from_destionation():
	if drag_source != null:
		drag_destination = null
	else:
		drag_source = null


func _on_button_selected(object):
	drag_source = object
	drag_icon = object.texture_normal
