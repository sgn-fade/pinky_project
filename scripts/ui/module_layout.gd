extends Control

func _ready():
	EventBus.connect("set_spell_icon_to_game", Callable(self, "_on_set_spell_icon_to_game"))
	EventBus.connect("remove_spell_icon_from_game", Callable(self, "_on_remove_spell_icon_from_game_ui"))
	
func _on_remove_spell_icon_from_game(button):
	get_node("%s" % button).texture_normal = null


func _on_set_spell_icon_to_game(module_icon, button):
	get_node("%s" % button).texture_normal = module_icon
