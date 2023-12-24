extends KinematicBody2D

func _ready():
	yield(get_tree().create_timer(10), "timeout")
	self.queue_free()
