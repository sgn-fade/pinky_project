extends StaticBody2D


func _ready():
	EventBus.connect("damage_to_enemy", Callable(self, "_on_damage_to_enemy"))

func _on_damage_to_enemy(body, damage, status):
	if body == self:
		destrory_self()
		
func destrory_self():
	$sprite.play("destroy")
	EventBus.disconnect("damage_to_enemy", Callable(self, "_on_damage_to_enemy"))
	$coll.set_deferred("disabled", true)
