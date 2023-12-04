#script for a group of undeads(skeletons)
extends Enemies
class_name Undeads

var type = "undead"
onready var sprite = $Sprite
onready var body = $Area2D

func _ready():
	pass 

func _process(delta):
	if global_position.distance_to(Player.get_position()) < 100:
		set_direction(Player.get_position() - global_position)
		move()
		sprite.play("idle")
	else:
		sprite.play("idle")


func move():
	swap_sprite_direction()
	#sprite.play("move")
	if self.hp > 0 && Player.get_hp() > 0 :
		move_and_slide((get_direction()).normalized() * speed)
		
func swap_sprite_direction():
	if get_direction().x <= 0:
		body.transform.x.x = 1
	elif get_direction().x > 0:
		body.transform.x.x = -1
