#script for a group of undeads(skeletons)
extends Enemies
class_name Undeads

var type = "undead"
@onready var sprite = $Sprite2D
@onready var body = $Area2D

func _ready():
	pass 




func move():
	swap_sprite_direction()
	#sprite.play("move")
	if self.hp > 0 && Player.get_hp() > 0 :
		set_velocity((get_direction()).normalized() * speed)
		move_and_slide()
		
func swap_sprite_direction():
	if get_direction().x <= 0:
		body.transform.x.x = 1
	elif get_direction().x > 0:
		body.transform.x.x = -1
