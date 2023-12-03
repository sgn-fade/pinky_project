extends "res://scripts/bullet.gd"

onready var barrel = get_node("/root/World/player/hands/heands_continuum_shotgun/pos")
var timer := Timer.new()

func _ready():
	timer.one_shot = false
	add_child(timer)
	max_distance = 10000
	global_position = barrel.global_position 
	mouse_pos = barrel.global_position
	$Area2D.connect("body_entered", self, "_on_body_entered")
	character_pos = Player.global_position 
	character_pos.y += 1.5
	mouse_pos.x += int(randi())%5
	mouse_pos.y += int(randi())%5
	mouse_pos.x -= 2.5
	mouse_pos.y -= 2.5
	$b.frame = randi()%5
	speed += randi()%30
	look_at(character_pos) 
	$particles.emitting = true


func delete():
	$b.queue_free()
	$Area2D.queue_free()
	$Light2D.queue_free()
	$particles.queue_free()
	self.speed = 0
	$end_particles.emitting = true
	timer.start(0.2)
	yield(timer, "timeout")
	queue_free()
