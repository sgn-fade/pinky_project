extends StaticBody2D


func _ready():
	EventBus.connect("generate_dungeon", self, "generate_dungeon")

func generate_dungeon():
	queue_free()
