extends Node

#player take damage from any source
signal player_take_damage(player_offcet_dir, damage) 
#enemy's damage value close sprite to show player damage value
signal show_damage_value(damage_label_instance, damage)
#signals to start event with altar  
signal spheres_donated(time)
signal survive_event_started(room_size, room_center, time)
#to emit enemy attract to source
signal pulls_body(body, position)
signal push_away_enemy(body, velocity)
signal crosshair_switch(type)


signal player_cast_spell(animation_time, animation_name)
signal hands_play_animation(animation_name)
signal switch_hands_stance(weapon)
signal spell_animation_ended()


signal dash_cooldown()
signal player_teleport(position)

signal show_module_stats_on_game_screen(module)
signal hide_module_stats_on_game_screen()


signal show_weapon_stats_on_game_screen(weapon)
signal hide_weapon_stats_on_game_screen()


signal weapon_in_inventory_choosed(weapon)



signal set_spell_icon_to_game(module_icon, button)
signal remove_spell_icon_from_game(button)

signal clear_spell_icons()


#inventory
signal add_item(item)


signal start_spell_cooldown(time, button)
signal damage_to_enemy(body, damage, status)


signal update_character_hp_bar_value(hp, max_hp)
signal update_character_mana_bar_value(mana, max_mana)


signal generate_dungeon()
signal go_to_hub()
signal load_game() #loaded start hub after press button in main menu

signal enemy_killed()

signal add_player_to_board(player_name)
