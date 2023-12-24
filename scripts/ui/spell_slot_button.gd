extends Control
var module = null
var modules_drop = load("res://scenes/modules_drop.tscn")
onready var new = get_node("button/new_label")
onready var main_button = get_node("button")
var equiped = false
var index = null


func init(module_new, cell_index):
	self.module = module_new
	self.index = cell_index
	set_button_texture()

func _process(delta):
	if $menu.animation == "open" and $menu.frame == 10:
		$options_with_module.visible = true
	else:
		$options_with_module.visible = false


func set_equiped(state):
	var equip_text = get_node("options_with_module/equip_button/new_label")
	if state:
		equip_text.text = "remove"
	else:
		equip_text.text  = "equip"
	self.equiped = state


func set_new_label(state):
	new.visible = state

func is_new():
	return new.visible

func _input(event):
	if (
		(Input.is_action_just_pressed("mouse_left_button") 
			or Input.is_action_just_pressed("mouse_right_button"))
		and not Rect2(Vector2(), main_button.rect_size).has_point(get_local_mouse_position())
		and $menu.playing
		):
		
		EventBus.emit_signal("spell_slot_button_unselected")
		if rect_scale.x > 1:
			rect_scale.x -= 0.1
			rect_scale.y -= 0.1
		$light.visible = false
		$menu.playing = false
		$menu.play("close")


func _on_drop_button_pressed():
	if equiped:
		return
	var modules_drops = modules_drop.instance()
	GlobalWorldInfo.get_world().add_child(modules_drops)
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
			


func _on_main_button_mouse_entered():
	new.visible = false
	$button.disconnect("mouse_entered", self, "_on_main_button_mouse_entered")


func set_button_texture():
	main_button = get_node("button")
	var icon = get_node("button/spell_icon")
	icon.texture = self.module.spell_icon
	main_button.texture_pressed = load("res://sprites/ui/%s_module_button_pressed.png" % module.rarity)
	main_button.texture_normal = load("res://sprites/ui/%s_module_button_state.png" % module.rarity)
	main_button.texture_hover = load("res://sprites/ui/%s_module_button_hover.png" % module.rarity)


func _on_equip_button_pressed():
	$menu.play("close")
	rect_scale.x += 0.1
	rect_scale.y += 0.1
	$light.visible = true
	EventBus.emit_signal("spell_slot_button_choosed", self, equiped)
