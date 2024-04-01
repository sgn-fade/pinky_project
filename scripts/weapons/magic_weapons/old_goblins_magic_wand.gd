extends "res://scripts/weapons/magic_weapons/magic_wands.gd"

func _init():
	super._init()
	var icon = load("res://sprites/ui/inventory/item_icons/item_icon_goblin's_wand.png")
	inventory_item = inventory_item_scene.new(self, "weapon", icon)
	#icon = load("res://sprites/weapons/magic_weapons/old_goblins_magic_wand.png")
