extends Area2D
var y_offcet = 0
var offcet_sign = 1

func _ready():
	self.position = Vector2(256.304, -187.419)
	
func _process(delta):
	if y_offcet >= 25:
		offcet_sign = -0.5
	elif y_offcet <= -5:
		offcet_sign = 0.5
	$kinematic.move_and_slide(Vector2(0, offcet_sign))
	y_offcet += offcet_sign
	if overlaps_body(Player):
		$kinematic/cake.frame = 1
		if Input.is_action_just_pressed("pick_up") && Player.get_hp() <= 35:
			EventBus.emit_signal("butt_heal")
			queue_free()
	else:
		$kinematic/cake.frame = 0
