extends Node2D

@onready var dungeon = load("res://scenes/locations/dungeon.tscn")
@onready var hub_zone = load("res://scenes/locations/hub_zone.tscn")
@onready var player = load("res://scenes/main_character.tscn")

var location

func _ready():
	#var screen_size = Vector2(1000,600)
	var screen_size = DisplayServer.screen_get_size()
	var window = get_window()
	window.size = screen_size
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
