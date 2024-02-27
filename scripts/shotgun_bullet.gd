extends "res://scripts/bullet.gd"

@onready var barrel = Player.get_node("hands/shotgun/pos")

func _ready():
	global_position = barrel.global_position 
	mouse_pos = barrel.global_position 
