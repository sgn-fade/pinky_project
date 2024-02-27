extends Node2D

@onready var sprite = $sprite

func _ready():
	EventBus.connect("hands_play_animation", Callable(self, "play_animation"))


func play_animation(animation_name):
	if animation_name == null:
		return
	sprite.play(animation_name)
	await sprite.animation_finished

