<?php
/**
* Plugin Name: truncate35
* Plugin URI: http://www.monsite.com/truncate35
* Description: Tronque un commentaire à 35 caractères
* Version: 1.0
* Author: Votre nom
* Author URI: http://www.monsite.com
*/

function truncate($content)
{
  $max = 35;
    if( strlen( $content ) > $max ) {
      return substr( $content, 0, $max ). " …";
    } 
    else {
      return $content;
    }
}

function red_salute($content)
{
  $word = array('salut', 'Salut');
  $change = array('<span style="color: red; font-weight: bold">salut</span>', 
                  '<span style="color: red; font-weight: bold">Salut</span>');
  $result = str_replace($word, $change, $content);
  return $result;
}

add_filter('comment_text', 'truncate');
add_filter('the_content', 'red_salute');

?>
