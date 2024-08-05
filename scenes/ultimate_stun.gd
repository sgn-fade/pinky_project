extends Node2D
var previousPos = {
	
}
func _ready():
	self.global_position = Player.get_position()

	
func _on_area_2d_body_entered(body):
	previousPos[body] = body.global_position

		 
func teleportToDimension():
	for entity in previousPos.keys():
		entity.global_position += Vector2(10000,10000)
	global_position += Vector2(10000,10000)

func backToWorld():
	for entity in previousPos.keys():
		entity.global_position = previousPos[entity]
	
	
	
