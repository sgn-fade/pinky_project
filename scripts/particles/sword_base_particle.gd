extends Node2D

func _ready():
	look_at(get_global_mouse_position())
	$anim.play("default")
	$sprite.frame = 0
	yield($sprite, "animation_finished")
	queue_free()


func _on_Area2D_body_entered(body):
	var damage = Player.get_weapon().damage
	if randf() * 100 < Player.get_weapon().critchance:
		damage *= 2
	EventBus.emit_signal("damage_to_enemy", body, damage, null)
