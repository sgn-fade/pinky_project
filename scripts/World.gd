extends Node2D
onready var fireball_spell = load("res://scripts/spells/fireball_spell.gd")
onready var fire_pillar_spell = load("res://scripts/spells/fire_pillar_spell.gd")
onready var fire_teleport_spell = load("res://scripts/spells/fire_teleport_spell.gd")
onready var fire_eye_spell = load("res://scripts/spells/fire_eye_spell.gd")
onready var fire_spear_spell = load("res://scripts/spells/fire_spear_spell.gd")
onready var dungeon = load("res://scenes/locations/dungeon.tscn")
onready var hub_zone = load("res://scenes/locations/hub_zone.tscn")
onready var weapon_drop = load("res://scenes/weapon_drop.tscn")

var location
func _ready():

	var drop = weapon_drop.instance()
	add_child(drop)
	drop.global_position = Vector2(40, -10)
	drop = weapon_drop.instance()
	add_child(drop)
	drop.global_position = Vector2(-40, -10)
	EventBus.emit_signal("add_module_to_place", fireball_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_pillar_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_teleport_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_eye_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_spear_spell.new(), true, "inventory", -1)
	EventBus.connect("generate_dungeon", self, "generate_dungeon")
	EventBus.connect("go_to_hub", self, "go_to_hub")

func _process(delta):
	pass

func generate_dungeon():
	location = dungeon.instance()
	add_child(location)
	location.generate_dungeon()

func go_to_hub():
	location.queue_free()
	location = hub_zone.instance()
	add_child(location)
	Player.set_position(Vector2.ZERO)
