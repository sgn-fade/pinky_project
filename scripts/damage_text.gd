extends Label

var damage = 0
var time_to_free = 0.5

func _ready():
	margin_top -= 10
	visible = false
	EventBus.connect("show_damage_value", self, "on_enemy_show_damage_value")


func _process(delta):
	time_to_free -= delta
	if time_to_free <= 0 && damage > 0:
		damage = 0
		visible = false


func on_enemy_show_damage_value(damage_label_instance, player_damage):
	if self == damage_label_instance:
		self.time_to_free = 0.5
		self.visible = true
		damage += player_damage
		text = String(-damage)
