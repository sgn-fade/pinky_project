extends StaticBody2D
@onready var sprite = $Aspell
var timer := Timer.new()

func _ready():
	EventBus.connect("hands_play_animation", Callable(self, "play_animation"))
	timer.one_shot = false
	add_child(timer)
	cast_animation()
	

func cast_animation():
	sprite.play("preparation")
	for i in 18:
		self.timer.start(0.06)
		await self.timer.timeout
		$light.energy += 0.07


func play_animation(time, animation_name):
	sprite.play(animation_name)
	timer.start(time)
	await timer.timeout
	cast_animation()
	EventBus.emit_signal("spell_animation_ended")


