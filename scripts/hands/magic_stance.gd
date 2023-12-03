extends "res://scripts/hands/clear_hands.gd"

func play_animation(animation_time, animation_name):
	.play_animation(animation_time, animation_name)
	if animation_name == "idle":
		$particles.gravity.x = 0
		$particles.gravity.y = -75
		$particles2.gravity.x = 0
		$particles2.gravity.y = -75
	if animation_name == "move":
		$particles.gravity.x = 0
		$particles.gravity.y = 0
		$particles2.gravity.x = 0
		$particles2.gravity.y = 0

func _process(delta):
	if sprite.animation == "idle" and (sprite.frame == 2 or sprite.frame == 3):
		$particles.position.y = 2
		$particles2.position.y = 2
	else:
		$particles.position.y = 1
		$particles2.position.y = 1

#
#func _ready():
#	EventBus.connect("hands_play_animation", self, "play_animation")
#	timer.one_shot = false
#	add_child(timer)
#	cast_animation()
#
#
#func cast_animation():
#	sprite.play("preparation")
#	for i in 18:
#		self.timer.start(0.06)
#		yield(self.timer, "timeout")
#		$light.energy += 0.07
#
#
#func play_animation(time, animation_name):
#	sprite.play(animation_name)
#	timer.start(time)
#	yield(timer, "timeout")
#	cast_animation()
#	EventBus.emit_signal("spell_animation_ended")
