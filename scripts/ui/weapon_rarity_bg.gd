extends Control
onready var main_button = $main_button


func show_weapon(weapon):
	$main_button/weapon_texture.texture = weapon.icon
	visible = true


func _on_unequip_button_pressed():
	visible = false
	EventBus.emit_signal("remove_weapon_from_slot")


func _on_main_button_pressed():
	$options_with_weapon.visible = true


func _input(event):
	if (
		(Input.is_action_just_pressed("mouse_left_button") 
			or Input.is_action_just_pressed("mouse_right_button"))
		and not Rect2($options_with_weapon.rect_position, $options_with_weapon.rect_size * $options_with_weapon.rect_scale).has_point(get_local_mouse_position())):
		$options_with_weapon.visible = false
