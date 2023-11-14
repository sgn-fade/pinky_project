extends Sprite

export var text : String

func _ready():
	get_node("Button_text").set_text(text)


