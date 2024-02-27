extends "res://scripts/weapons/magic_weapons/magic_weapon.gd"
var damage_scale = 0
var magic_wands_affixes = [
	"magic_damage_increese"
]
func magic_damage_increese():
	Player.set_magic_damage(Player.get_magic_damage() + damage_scale)

func _init():
	super._init()
