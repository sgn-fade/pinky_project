extends Goblins
class_name Goblin_mage
var fireball = load("res://scenes/particles/enemy's_fireball.tscn")
var fireball_cast_cooldown = 2
var fire_elemental = load("res://scenes/enemies/elementals/fire_elemental.tscn")
enum States{
	IDLE,
	SEARCH,
	CASTING,
	RUNNING,
	SUMMONING,
}
var current_state = States.IDLE
var acceleration = 1
var elemental_cooldown = 10

func _ready():
	hp = 10
	self.hp_bar.max_value = hp * 10
	self.hp_bar.value = hp * 10
	self.white_animation_bar.max_value = hp * 10
	self.white_animation_bar.value = hp * 10
	speed = 40


func _process(delta):
	fireball_cast_cooldown -= delta
	elemental_cooldown -= delta
	z_index = global_position.y / 2
	match current_state:
		States.IDLE:
			searching_player(delta)
		States.SEARCH:
			pass
		States.CASTING:
			pass
		States.RUNNING:
			run()
		States.SUMMONING:
			pass

func idle():
	await get_tree().create_timer(randf()).timeout
	current_state = States.IDLE
func searching_player(delta):
	$sprite.play("move")
	if current_state != States.SEARCH:
		current_state = States.SEARCH
		direction = randomize_direction().normalized() * (randi() % 10)
		var t = 0
		while(t < 1):
			t += delta
			velocity = velocity.lerp(direction, t)
			
			set_velocity(velocity)
			move_and_slide()
			velocity = velocity
			if global_position.distance_to(Player.get_position()) < 300 and fireball_cast_cooldown <= 0:
					cast_fireball()
					#summon_elemental()
			await get_tree().create_timer(delta).timeout
		await idle()
		current_state = States.IDLE


func randomize_direction():
	return Vector2(randi() % 3 - 1, randi() % 3 - 1)


func cast_fireball():
	if fireball_cast_cooldown <= 0 && current_state != States.CASTING:
		current_state = States.CASTING
		var fireball_particle = fireball.instantiate()
		fireball_particle.global_position = $fireball_pos.global_position
		GlobalWorldInfo.get_world().add_child(fireball_particle)
		$anim.play("attack")
		await $sprite.animation_finished
		fireball_cast_cooldown = 2
		idle()

func run():
	pass

func summon_elemental():
	if elemental_cooldown <= 0 && current_state != States.SUMMONING:
		current_state = States.SUMMONING
		var summon = fire_elemental.instantiate()
		GlobalWorldInfo.get_world_3d().add_child(summon)
		summon.global_position = self.global_position + Vector2(10, 0)
		elemental_cooldown = randi()%30+10
		idle()
