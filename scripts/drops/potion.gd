extends Node

var inventory_item_scene = load("res://scripts/drops/inventory_item.gd")
var inventory_item = null

func _init():
	var icon = load("res://sprites/ui/inventory/item_icons/healing potion.png")
	inventory_item = inventory_item_scene.new(self, "consumable", icon)

func use():
	Player.update_hp(5)
