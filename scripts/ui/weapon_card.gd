extends Control
var weapon_drop = load("res://scenes/weapon_drop.tscn")
var weapon = null

func init(weapon_new):
	self.weapon = weapon_new

func _on_drop_button_pressed():
	var drop = weapon_drop.instance()
	GlobalWorldInfo.get_world().add_child(drop)
	drop.global_position = Player.get_position()
	drop.weapon = weapon
	drop.z_index = Player.get_z_index()
	queue_free()
