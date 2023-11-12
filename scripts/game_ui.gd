extends Control
onready var orb_label = get_node("blood_orb_count/Label")

func _ready():
	EventBus.connect("set_spell_icon_to_game_ui", self, "_on_set_spell_icon_to_game_ui")
	EventBus.connect("start_spell_cooldown", self, "_on_start_spell_cooldown")
	EventBus.connect("remove_spell_icon_from_game_ui", self, "_on_remove_spell_icon_from_game_ui")
	
	EventBus.connect("show_module_stats_on_game_screen", self, "_on_show_module_stats_on_game_screen")
	EventBus.connect("hide_module_stats_on_game_screen", self, "_on_hide_module_stats_on_game_screen")
	
	EventBus.connect("show_weapon_stats_on_game_screen", self, "_on_show_weapon_stats_on_game_screen")
	EventBus.connect("hide_weapon_stats_on_game_screen", self, "_on_hide_weapon_stats_on_game_screen")
	

func _process(delta):
	orb_label.text = "4"


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


func _on_remove_spell_icon_from_game_ui(button):
	get_node("spell_slot_panel/%s" % button).texture_progress = null


func _on_set_spell_icon_to_game_ui(module_icon, button):
	get_node("spell_slot_panel/%s" % button).texture_progress = module_icon


func _on_start_spell_cooldown(time, button):
	var timer := Timer.new()
	timer.one_shot = false
	add_child(timer)
	var bar = get_node("spell_slot_panel/%s" % button)
	bar.max_value = 60 * time
	bar.value = 0
	for i in bar.max_value:
		bar.value += 1
		timer.start(0.016)
		yield(timer, 'timeout')
	timer.queue_free()
