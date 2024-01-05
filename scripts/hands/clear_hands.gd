extends Node2D

@onready var sprite = $sprite

func _ready():
	EventBus.connect("hands_play_animation", Callable(self, "play_animation"))


func play_animation(animation_time, animation_name):
	sprite.play(animation_name)

