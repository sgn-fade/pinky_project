extends Node2D

@onready var fireball_spell = load("res://scripts/spells/fireball_spell.gd")
@onready var fire_pillar_spell = load("res://scripts/spells/fire_pillar_spell.gd")
@onready var fire_teleport_spell = load("res://scripts/spells/fire_teleport_spell.gd")
@onready var fire_eye_spell = load("res://scripts/spells/fire_eye_spell.gd")
@onready var fire_spear_spell = load("res://scripts/spells/fire_spear_spell.gd")
@onready var smite = load("res://scripts/spells/melee_spells/smite.gd")
@onready var dungeon = load("res://scenes/locations/dungeon.tscn")
@onready var hub_zone = load("res://scenes/locations/hub_zone.tscn")
@onready var player = load("res://scenes/main_character.tscn")

var location

func _ready():
	Player.spawn()
	EventBus.connect("generate_dungeon", Callable(self, "generate_dungeon"))
	EventBus.connect("load_game", Callable(self, "load_game"))
	EventBus.connect("go_to_hub", Callable(self, "go_to_hub"))
	EventBus.emit_signal("load_game")


func generate_dungeon():
	location = dungeon.instantiate()
	$location.add_child(location)
	#location.generate_dungeon()


func go_to_hub():
	if location != null:
		location.queue_free()
	location = hub_zone.instantiate()
	$location.add_child(location)
	Player.set_position(Vector2.ZERO)


func load_game():
	go_to_hub()
	EventBus.emit_signal("add_module_to_place", fireball_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_pillar_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_teleport_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_eye_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", fire_spear_spell.new(), true, "inventory", -1)
	EventBus.emit_signal("add_module_to_place", smite.new(), true, "inventory", -1)
