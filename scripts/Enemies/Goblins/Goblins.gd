#script for a group of goblins 
extends Enemies
class_name Goblins


var type = "goblin"
func _ready():
	pass
#дропают понетки, искать гоблинов и ходить кучкой

func spawn_drop():
	var gold = load("res://scenes/drops/gold_drop.tscn")
	var gold_drop = gold.instance()
	GlobalWorldInfo.get_world().add_child(gold_drop)
	gold_drop.global_position = self.global_position
	
