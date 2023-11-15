extends Node2D
onready var enemy = preload("res://scenes/enemy.tscn")
onready var goblin = preload("res://scenes/goblin.tscn")

var spawn_cooldown = 0
var timer := Timer.new()

func _ready():
	timer.one_shot = false
	add_child(timer)
	EventBus.connect("character_dead", self, "on_character_dead")


func on_character_dead():
	EventBus.emit_signal("turn_to_red_fire")
	for i in 5:
		$CanvasModulate.color = $CanvasModulate.color.darkened(0.02)
		timer.start(0.1)
		yield(timer, "timeout")

