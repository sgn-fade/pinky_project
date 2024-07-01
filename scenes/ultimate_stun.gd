extends Node2D
var previousPos = {
	
}
# Called when the node enters the scene tree for the first time.
func _ready():
	self.global_position = Player.get_position()
	Player.set_z_index(2)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass 
	
func _on_area_2d_body_entered(body):
	previousPos[body] = body.global_position
	print(body)
	
		 
func teleportToDimension():
	var massive = previousPos.keys()
	for entity in massive:
		entity.global_position += Vector2(10000,10000)
	global_position += Vector2(10000,10000)
func backToWorld():
	var massive = previousPos.keys()
	for entity in massive:
		entity.global_position = previousPos[entity]
	
	
	
