extends "res://scripts/hands/melee_hands.gd"

func _on_area_body_entered(body):
	var damage = Player.get_weapon().damage
	if randf() * 100 < Player.get_weapon().critchance:
		damage *= 2
	EventBus.emit_signal("damage_to_enemy", body, damage, null)


