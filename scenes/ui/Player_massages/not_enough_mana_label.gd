extends Node2D

func _ready():
	$player.play("main")
	await $player.animation_finished
	queue_free()

