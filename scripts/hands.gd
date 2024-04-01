extends Node2D
@onready var shotgun = preload("res://scenes/shotgun.tscn")
@onready var magic = preload("res://scenes/hands/magic_hands.tscn")
@onready var clear = preload("res://scenes/hands/clear_hands.tscn")
@onready var melee = preload("res://scenes/hands/melee_hands.tscn")
@onready var sword = preload("res://scenes/hands/sword_hands.tscn")
enum States{
	EMPTY,
	MAGIC,
	GUN,
	MELEE,
}
var current_state = States.GUN

func _ready():
	switch_hands(clear)
	EventBus.connect("switch_hands_stance", Callable(self, "_on_switch_hands_stance"))

func _on_switch_hands_stance(weapon):
	if weapon == null:
		switch_hands(clear)
		return
	match weapon.get_type():
		"magic":
			current_state = States.MAGIC
			switch_hands(magic)
		"melee":
			current_state = States.MELEE
			switch_hands(sword)


func switch_hands(type):
	if get_child_count() > 0:
		get_child(0).queue_free()
	add_child(type.instantiate())

