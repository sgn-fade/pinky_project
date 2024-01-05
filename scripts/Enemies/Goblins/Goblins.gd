#script for a group of goblins 
extends Enemies
class_name Goblins


var gold_drop = preload("res://scenes/drops/gold_drop.tscn")

var type = "goblin"


func _ready():
	pass

func spawn_drop():
	randomize()
	var modules_drops = modules_drop.instantiate()
	GlobalWorldInfo.get_world_3d().add_child(modules_drops)
	modules_drops.global_position = self.global_position
	modules_drops.z_index = self.z_index
	for i in randi() % 6:
		var coins = gold_drop.instantiate()
		GlobalWorldInfo.get_world_3d().add_child(coins)
		coins.global_position = self.global_position + Vector2(randi()%11 - 5, randi()%11 - 5)
		coins.z_index = self.z_index
