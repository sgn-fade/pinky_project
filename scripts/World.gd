extends Node2D
onready var module = load("res://scripts/spells/fire_eye_spell.gd")
onready var module1 = load("res://scripts/spells/fireball_spell.gd")
func _ready():
	EventBus.emit_signal("add_module_to_inventory", module.new(), true)

func _process(delta):
	pass
