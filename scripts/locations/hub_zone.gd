extends StaticBody2D


func _ready():
	EventBus.connect("generate_dungeon", Callable(self, "generate_dungeon"))

func generate_dungeon():
	queue_free()
