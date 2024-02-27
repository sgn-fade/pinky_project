extends Label
var timer := Timer.new()

func _ready():
	timer.one_shot = false
	add_child(timer)
	EventBus.connect("spheres_donated", Callable(self, "_on_spheres_donated"))
	

func _process(delta):
	text = str(int(timer.time_left / 60)) + ":" + String("%02d" % (int(timer.time_left) % 60))
	if timer.time_left <= 0.1:
		timer.stop()

func _on_spheres_donated(time):
	visible = true
	timer.start(time)
	
