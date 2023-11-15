extends "res://scripts/bullet.gd"

onready var barrel = get_node("/root/World/main_character/hands/heands_continuum_shotgun/pos")
var timer := Timer.new()


func _ready():
	$b.play("bullet")
	timer.one_shot = false
	add_child(timer)
	speed = 100
	global_position = barrel.global_position 
	mouse_pos = barrel.global_position
	$Area2D.connect("body_entered", self, "_on_body_entered")
	character_pos = Player.global_position 
	character_pos.y += 1.5
	look_at(character_pos) 
	
	

func delete():
	$b.play("explode")
	timer.start(0.33)
	yield(timer, "timeout")
	$b.queue_free()
	$end_partcl_pink.emitting = true
	$end_partcl_white.emitting = true
	$Area2D.queue_free()
	for i in 4:
		$Light2D.texture_scale += 0.02
		timer.start(0.04)
		yield(timer, "timeout")
	timer.start(0.5)
	yield(timer, "timeout")
	queue_free()
