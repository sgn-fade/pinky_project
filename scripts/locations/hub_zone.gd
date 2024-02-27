extends StaticBody2D
var goblin_sword = load("res://scripts/weapons/melee/sword.gd")

func _ready():
	EventBus.emit_signal("weapon_in_inventory_choosed", goblin_sword.new())
	EventBus.connect("generate_dungeon", Callable(self, "generate_dungeon"))

func generate_dungeon():
	queue_free()
