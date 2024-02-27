extends Control
var weapon_drop = load("res://scenes/weapon_drop.tscn")
var weapon = null
@onready var main_button = $main_button


func init(weapon_new):
	self.weapon = weapon_new


func _on_drop_button_pressed():
	var drop = weapon_drop.instantiate()
	GlobalWorldInfo.get_world_3d().add_child(drop)
	drop.global_position = Player.get_position()
	drop.weapon = weapon
	drop.z_index = Player.get_z_index()
	queue_free()


func _on_mainbutton_pressed():
	$options_with_weapon.visible = true


func _input(event):
	if (
		(Input.is_action_just_pressed("mouse_left_button") 
			or Input.is_action_just_pressed("mouse_right_button"))
		and not Rect2(Vector2(), main_button.size).has_point(get_local_mouse_position())
		):
		$options_with_weapon.visible = false


func _on_equip_button_pressed():
	$options_with_weapon.visible = false
	EventBus.emit_signal("weapon_in_inventory_choosed", weapon)
	queue_free()
