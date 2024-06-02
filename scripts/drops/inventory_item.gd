extends Node

var data_type = null
var data = null
var icon = null
var background = null
var is_stackable = false
var value = null

func _init(new_data, new_data_type, new_icon, new_background = null):
	data_type = new_data_type
	data = new_data
	icon = new_icon
	background = new_background
