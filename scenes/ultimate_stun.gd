extends Node2D
var previousPos = {
	
}
func _ready():
	self.global_position = Player.get_position()
	Player.set_z_index(2)


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
	
	
	
