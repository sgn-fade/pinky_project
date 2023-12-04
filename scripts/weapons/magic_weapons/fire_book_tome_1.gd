extends "res://scripts/weapons/magic_weapons/magic_wands.gd"
var max_mana_buff = 20
func _init():
	icon = load("res://sprites/weapons/magic_weapons/books/fire_book_tome_1.png")
	damage_scale = 2

func get_max_mana_buff():
	return max_mana_buff
