extends Control


func _input(event):
	if Input.is_action_just_pressed("slot_1"):
		Player.set_weapon(Player.get_weapon_1())
		$slot1.z_index = 1
		$slot2.z_index = 0
	if Input.is_action_just_pressed("slot_2"):
		Player.set_weapon(Player.get_weapon_2())
		$slot1.z_index = 0
		$slot2.z_index = 1
		
