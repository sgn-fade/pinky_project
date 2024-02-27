extends Control
@onready var orb_label = get_node("blood_orb_count/Label")


var Bars = {
	"slot1" = null,
	"slot2" = null,
	"slot3" = null,
	"slot4" = null,
	"slot5" = null,
	"slot6" = null,
}
func _ready():
	EventBus.connect("set_spell_icon_to_game", Callable(self, "_on_set_spell_icon_to_game"))
	EventBus.connect("start_spell_cooldown", Callable(self, "_on_start_spell_cooldown"))
	EventBus.connect("remove_spell_icon_from_game", Callable(self, "_on_remove_spell_icon_from_game"))
	EventBus.connect("clear_spell_icons", Callable(self, "clear_spell_icons"))
	
	EventBus.connect("show_module_stats_on_game_screen", Callable(self, "_on_show_module_stats_on_game_screen"))
	EventBus.connect("hide_module_stats_on_game_screen", Callable(self, "_on_hide_module_stats_on_game_screen"))
	EventBus.connect("show_weapon_stats_on_game_screen", Callable(self, "_on_show_weapon_stats_on_game_screen"))
	EventBus.connect("hide_weapon_stats_on_game_screen", Callable(self, "_on_hide_weapon_stats_on_game_screen"))
	

func _process(delta):
	for bar in $spell_slot_panel.get_children():
		if Bars[bar.name] == null:
			continue
		if bar.value < bar.max_value:
			bar.value += delta * 1000
			Bars[bar.name].set_cooldown_time(delta)


func _on_show_module_stats_on_game_screen(module):
	$module_discription.visible = true
	$module_discription/module_icon.texture = module.spell_icon
func _on_hide_module_stats_on_game_screen():
	$module_discription.visible = false

func _on_show_weapon_stats_on_game_screen(weapon):
	$weapon_discription.visible = true
	$weapon_discription/weapon_texture.texture = weapon.icon
func _on_hide_weapon_stats_on_game_screen():
	$weapon_discription.visible = false


func _on_remove_spell_icon_from_game(button):
	get_node("spell_slot_panel/%s" % button).texture_progress = null
	Bars[button] = null

func _on_set_spell_icon_to_game(module, button):
	var bar = get_node("spell_slot_panel/%s" % button)
	bar.max_value = module.get_max_cooldown_time() * 1000
	
	if module.get_ready():
		bar.value = bar.max_value
	else:
		bar.value = module.get_cooldown_time() * 1000
	bar.texture_progress = module.spell_icon
	Bars[button] = module

func clear_spell_icons():
	for node in $spell_slot_panel.get_children():
		node.texture_progress = null
	
func _on_start_spell_cooldown(button):
	var bar = get_node("spell_slot_panel/%s" % button)
	bar.value = Bars[button].get_cooldown_time() * 1000

