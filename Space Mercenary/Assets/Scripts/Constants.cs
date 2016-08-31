using UnityEngine;
using System.Collections;

public static class Constants {

	// Name of players slots.
	public static string EMPTY_SLOT = "(Empty)";
	public static string PLAYER_1 = "Player1";
	public static string PLAYER_2 = "Player2";
	public static string PLAYER_3 = "Player3";
	public static string PLAYER_4 = "Player4";
	
	// Name of scences.
	public static string MANAGE_PLAYERS = "Manage players";
	public static string LOBBY = "Lobby";
	public static string MAIN_MENU = "Main menu";
	public static string LEVEL_MENU = "Level menu";
	public static string NEW_PLAYER = "New player";
	public static string SHOP = "Shop";
	public static string TEST_LEVEL = "Test level";
	public static string MULTIPLAYER = "Multiplayer";

	// Name of enemy behaviours.
	public static string RIGHT_DIAGONAL_BEHAVIOUR = "RightDiagonalEnemyBehaviour";
	public static string LEFT_DIAGONAL_BEHAVIOUR = "LeftDiagonalEnemyBehaviour";
	public static string BASIC_BEHAVIOUR = "BasicEnemyBehaviour";
	public static string SIDE_TO_SIDE_BEHAVIOUR = "SideToSideEnemyBehaviour";
	public static string RANDOM_BEHAVIOUR = "RandomEnemyBehaviour";

	// Manage game.
	public static string PLAYER_INFO = "PlayerInfo";
	public static string LEVEL_CONFIGURATION = "Level configuration";
	public static string ENEMY_PARENT = "Enemy parent";
	public static string PLAYER_START_POINT = "Player start point";
	public static string NUMBER = "Number";
	public static string ENEMY_TEXT = "enemy";
	public static string BOSS_TEXT = "boss";
	public static string TIME_ATTRIBUTE = "time";
	public static string TYPE_ATTRIBUTE = "type";
	public static string BEHAVIOUR_ATTRIBUTE = "behaviour";
	public static string X_ATTRIBUTE = "x";
	public static string Y_ATTRIBUTE = "y";

	// Tags.
	public static string ENEMY_TAG = "Enemy";
	public static string ENEMY_PROJECTILE_TAG = "EnemyProjectile";
	public static string PLAYER_TAG = "Player";
	public static string PLAYER_PROJECTILE_TAG = "PlayerProjectile";
	public static string PLAYER2_TAG = "Player2";
	public static string ASTEROID_TAG = "Asteroid";

	// Paths.
	public static string SHIP_RESOURCES_PATH = "Prefabs/Ships";
	public static string SLASH = "/";
	public static string PATH_TO_LEVELS = "Assets/Resources/";
	public static string LEVEL_TEXT = "Levels/Level";
	public static string XML_EXTENSION = ".xml";

	// Load strings.
	public static string PLAYER_NAME = "Name";
	public static string PLAYER_MONEY = "Money";

	// Initial configuration.
	public static int INITIAL_MONEY = 10000;
	public static string TRUE = bool.TrueString;
	public static string FALSE = bool.FalseString;

	// Menu strings.
	public static string TEXT = "Text";

	// Shop texts.
	public static string SHOP_INITIAL_MESSAGE = "Welcome!";
	public static string DOLLAR_SYMBOL = "$";
	public static string MONEY_TEXT = "Money: ";
	public static string SHIP_TEXT = "Ship: ";
	public static string PRICE_TEXT = "Price: ";

	// Level texts.
	public static string HEALTH = "Health";
	public static string HEALTH_SYMBOL = "+ ";
	public static string SHIELD = "Shield";
	public static string SHIELD_SYMBOL = "O ";

	// Miscelaneus.
	public static string EMPTY_STRING = "";
	
}
