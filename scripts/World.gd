extends Node2D
onready var module = load("res://scripts/spells/fire_spear_spell.gd")
onready var module1 = load("res://scripts/spells/fireball_spell.gd")
func _ready():
	EventBus.emit_signal("add_module_to_place", module.new(), true, "inventory")



func _process(delta):
	pass
