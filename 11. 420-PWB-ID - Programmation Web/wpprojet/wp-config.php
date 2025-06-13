<?php
/**
 * La configuration de base de votre installation WordPress.
 *
 * Ce fichier contient les réglages de configuration suivants : réglages MySQL,
 * préfixe de table, clés secrètes, langue utilisée, et ABSPATH.
 * Vous pouvez en savoir plus à leur sujet en allant sur
 * {@link http://codex.wordpress.org/fr:Modifier_wp-config.php Modifier
 * wp-config.php}. C’est votre hébergeur qui doit vous donner vos
 * codes MySQL.
 *
 * Ce fichier est utilisé par le script de création de wp-config.php pendant
 * le processus d’installation. Vous n’avez pas à utiliser le site web, vous
 * pouvez simplement renommer ce fichier en "wp-config.php" et remplir les
 * valeurs.
 *
 * @package WordPress
 */

// ** Réglages MySQL - Votre hébergeur doit vous fournir ces informations. ** //
/** Nom de la base de données de WordPress. */
define( 'DB_NAME', 'projetpwb_db' );

/** Utilisateur de la base de données MySQL. */
define( 'DB_USER', 'user1' );

/** Mot de passe de la base de données MySQL. */
define( 'DB_PASSWORD', '1234' );

/** Adresse de l’hébergement MySQL. */
define( 'DB_HOST', 'localhost' );

/** Jeu de caractères à utiliser par la base de données lors de la création des tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** Type de collation de la base de données.
  * N’y touchez que si vous savez ce que vous faites.
  */
define('DB_COLLATE', '');

/**#@+
 * Clés uniques d’authentification et salage.
 *
 * Remplacez les valeurs par défaut par des phrases uniques !
 * Vous pouvez générer des phrases aléatoires en utilisant
 * {@link https://api.wordpress.org/secret-key/1.1/salt/ le service de clefs secrètes de WordPress.org}.
 * Vous pouvez modifier ces phrases à n’importe quel moment, afin d’invalider tous les cookies existants.
 * Cela forcera également tous les utilisateurs à se reconnecter.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         'v2mQ7G  `g(m2N|c@Yp^R:2E0[foJr{e8|>?_ ~j,JM`oSSO-DvtPfDqrOz,+SAG' );
define( 'SECURE_AUTH_KEY',  'NYJIK)n0P|zQNp0VF/PUeVO8-o@|&TBtI&fjMM9&R.y!V6IQI$vg.:Q6T8 x,ig&' );
define( 'LOGGED_IN_KEY',    '4t>BH)DW@N|MXtbt~+kb`jr4w?35nd!&<c2Yd-^F=8RRu_MO,H}BZiL&Qqu9{Hjk' );
define( 'NONCE_KEY',        ' ?;#U9Y=!0$vTS^>O0`o3bP~hRHz`8GLPtW^k%1<s8s$*`d+6.8gg+h8QJ;lQK<N' );
define( 'AUTH_SALT',        'l}IqbhwL`bOYJs=`*>19jlLr G3Ag.9VwAthW1)=RD5stmwhY<oD,H2^Q{5<*^~A' );
define( 'SECURE_AUTH_SALT', '=eH%&>e@g`#CuF!N{`vx.R3%Y|%no)M!no)4<UB.YY$VWP%S5]hiyGH)YpCknY@2' );
define( 'LOGGED_IN_SALT',   'qaN_mLOQKA4S>:TfW(LzfKbjuK`co^z W6ZaftXY8In,I%b~B8Ky-_dBj3E6PND@' );
define( 'NONCE_SALT',       '?.C&K7$}OH{xC8526c{D_GCy|1X;rRWIh%t/6cUpExz0ngyqWv8A#i64GNfA/k_d' );
/**#@-*/

/**
 * Préfixe de base de données pour les tables de WordPress.
 *
 * Vous pouvez installer plusieurs WordPress sur une seule base de données
 * si vous leur donnez chacune un préfixe unique.
 * N’utilisez que des chiffres, des lettres non-accentuées, et des caractères soulignés !
 */
$table_prefix = 'wp_';

/**
 * Pour les développeurs : le mode déboguage de WordPress.
 *
 * En passant la valeur suivante à "true", vous activez l’affichage des
 * notifications d’erreurs pendant vos essais.
 * Il est fortemment recommandé que les développeurs d’extensions et
 * de thèmes se servent de WP_DEBUG dans leur environnement de
 * développement.
 *
 * Pour plus d’information sur les autres constantes qui peuvent être utilisées
 * pour le déboguage, rendez-vous sur le Codex.
 *
 * @link https://codex.wordpress.org/Debugging_in_WordPress
 */
define('WP_DEBUG', false);

/* C’est tout, ne touchez pas à ce qui suit ! Bonne publication. */

/** Chemin absolu vers le dossier de WordPress. */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/** Réglage des variables de WordPress et de ses fichiers inclus. */
require_once(ABSPATH . 'wp-settings.php');
