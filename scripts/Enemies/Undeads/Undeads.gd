#script for a group of undeads(skeletons)
extends Enemies
class_name Undeads

var type = "undead"
onready var sprite = $Sprite

func _ready():
	pass 

func _process(delta):
	if global_position.distance_to(Player.get_position()) < 100:
		move(Player.get_position() - global_position)
		sprite.play("idle")
	else:
		sprite.play("idle")


func move(direction):
	#sprite.play("move")
	if self.hp > 0 && Player.get_hp() > 0 :
		move_and_slide((direction).normalized() * speed)

