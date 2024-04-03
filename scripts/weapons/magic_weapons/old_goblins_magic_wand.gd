extends "res://scripts/weapons/magic_weapons/magic_wands.gd"

func _init():
	super._init()
	texture = load("res://sprites/weapons/magic_weapons/old_goblins_magic_wand.png")
	var inv_icon = load("res://sprites/ui/inventory/item_icons/item_icon_goblin's_wand.png")
	inventory_item = inventory_item_scene.new(self, "weapon", inv_icon)

