extends Node

var Buttons_binds = {
	"slot1" : "X",
	"slot2" : "Q",
	"slot3" : "mouse_left_button",
	"slot4" : "mouse_right_button",
	"slot5" : "R",
	"slot6" : "C",
}

var Reverse_buttons_binds = {}

func _ready():
	for key in Buttons_binds.keys():
		Reverse_buttons_binds[Buttons_binds[key]] = key
