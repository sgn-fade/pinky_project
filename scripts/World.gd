extends Node2D
onready var module = load("res://scripts/spells/fire_spear_spell.gd")
onready var module1 = load("res://scripts/spells/fireball_spell.gd")
onready var module2 = load("res://scripts/spells/fire_teleport_spell.gd")
func _ready():

	EventBus.emit_signal("add_module_to_place", module.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", module2.new(), true, "inventory", -1)


func _process(delta):
	pass
