extends Control
var module = null
var modules_drop = load("res://scenes/modules_drop.tscn")
var mouse_focus = false
onready var new = $button/new_label
onready var icon = get_node("button/spell_icon")
onready var main_button = get_node("button")


func _process(delta):
	if $menu.animation == "open" and $menu.frame == 10:
		$options_with_module.visible = true
	else:
		$options_with_module.visible = false


func _input(event):
	if ((Input.is_action_just_pressed("mouse_left_button") or Input.is_action_just_pressed("mouse_right_button"))
	and not Rect2(Vector2(), main_button.rect_size).has_point(get_local_mouse_position())
	and $menu.playing):
		$menu.playing = false
		$menu.play("close")
		



func _on_drop_button_pressed():
	var modules_drops = modules_drop.instance()
	GlobalWorld.add_child(modules_drops)
	modules_drops.global_position = Player.get_position()
	modules_drops.module = module
	modules_drops.z_index = Player.get_z_index()
	queue_free()



func _on_main_button_pressed():
	if Input.is_action_just_released("mouse_right_button"):
		if $menu.animation == "open":
			$menu.play("close")
		else:
			$menu.play("open")
			yield(get_tree().create_timer(0.3), "timeout")
			
	EventBus.emit_signal("spell_slot_button_pressed", self)


func _on_main_button_mouse_entered():
	new.visible = false
	$button.disconnect("mouse_entered", self, "_on_main_button_mouse_entered")

func set_button_texture(new_module):
	module = new_module
	icon.texture = module.spell_icon
	main_button.texture_pressed = load("res://sprites/ui/%s_module_button_pressed.png" % module.rarity)
	main_button.texture_normal = load("res://sprites/ui/%s_module_button_state.png" % module.rarity)
	main_button.texture_hover = load("res://sprites/ui/%s_module_button_hover.png" % module.rarity)
