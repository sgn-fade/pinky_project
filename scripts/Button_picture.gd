extends Sprite

export var text : String

# Called when the node enters the scene tree for the first time.
func _ready():
	get_node("Button_text").set_text(text)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
