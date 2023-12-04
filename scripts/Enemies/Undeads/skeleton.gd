extends Undeads
class_name Skelet
var skelet_drop = load("res://scenes/drops/skeleton_drop.tscn")
func _process(delta):
	pass
	
func _ready():
	sprite = $body/Sprite
	body = $body
	hp = 15
	self.hp_bar.max_value = hp * 10
	self.hp_bar.value = hp * 10
	self.white_animation_bar.max_value = hp * 10
	self.white_animation_bar.value = hp * 10
	speed = 35
	enemy_damage = 4
	$body.connect("body_entered", self, "skeleton_attack")
	
	
	
func skeleton_attack():
	if global_position.distance_to(Player.get_position()) < 10:
		var player_offcet_dir = (-(self.global_position - Player.get_position()).normalized())
		EventBus.emit_signal("player_take_damage", player_offcet_dir, 4)

func spawn_drop():
	randomize()
	var modules_drops = modules_drop.instance()
	GlobalWorldInfo.get_world().add_child(modules_drops)
	modules_drops.global_position = self.global_position
	modules_drops.z_index = self.z_index
	for i in randi() % 4 + 1:
		var bones = skelet_drop.instance()
		GlobalWorldInfo.get_world().add_child(bones)
		bones.rotation_degrees = randi() % 180
		bones.global_position = self.global_position + Vector2(randi()%11 - 5, randi()%11 - 5)
		bones.z_index = self.z_index
