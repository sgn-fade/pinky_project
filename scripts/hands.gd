extends Node2D
onready var shotgun = preload("res://scenes/heands_continuum_shotgun.tscn")
onready var spell = preload("res://scenes/h_spell.tscn")

enum States{
	SHOTGUN
	FIREBALL
}
var current_state = States.SHOTGUN

func _ready():
	
	add_child(spell.instance())


func _process(delta):
	
	change_stance()

func change_stance():
	if Input.is_action_just_pressed("spell_slot") && current_state != States.FIREBALL:
		current_state = States.FIREBALL
		var spell_instance = spell.instance()
		remove_child(get_child(0))
		add_child(spell.instance())
		
		
	if Input.is_action_just_pressed("slot_1") && current_state != States.SHOTGUN:
		current_state = States.SHOTGUN
		var shotgun_instance = shotgun.instance()
		remove_child(get_child(0))
		add_child(shotgun.instance())
		

func clear_hands():
	if current_state == States.SHOTGUN:
		$shotgun.queue_free()
	elif  current_state == States.FIREBALL:
		$spell.queue_free()

