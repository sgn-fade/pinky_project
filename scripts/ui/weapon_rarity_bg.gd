extends Control
@onready var main_button = $main_button


func show_weapon(weapon):
	if weapon == null:
		return
	$main_button/weapon_texture.texture = weapon.texture
	visible = true

